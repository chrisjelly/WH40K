using JellyDev.WH40K.Infrastructure.Database.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Events;
using System.Collections.ObjectModel;
using System.Data;

namespace JellyDev.WH40K.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            string connectionStringRead = Configuration.GetConnectionString("JellyDevRead");
            builder.RegisterModule(new Api.AutofacModule(connectionStringRead));
            builder.RegisterModule(new Infrastructure.AutofacModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JellyDev.WH40K.Api", Version = "v1" });
            });

            // Database
            string connectionStringWrite = Configuration.GetConnectionString("JellyDevWrite");
            services.AddDbContextPool<StratagemDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(connectionStringWrite);
            });
            services.AddDbContextPool<FactionDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(connectionStringWrite);
            });

            // Logger
            var sinkOpts = new MSSqlServerSinkOptions();
            sinkOpts.TableName = "Logs";
            sinkOpts.AutoCreateSqlTable = true;
            var columnOpts = new ColumnOptions();
            columnOpts.Store.Remove(StandardColumn.Properties);
            columnOpts.Store.Add(StandardColumn.LogEvent);
            columnOpts.LogEvent.DataLength = 2048;
            columnOpts.TimeStamp.NonClusteredIndex = true;
            columnOpts.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn {
                    ColumnName = "RequestType", 
                    PropertyName = "RequestType", 
                    DataType = SqlDbType.NVarChar, 
                    DataLength = 50
                }
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo
                .MSSqlServer(
                    connectionString: connectionStringWrite,
                    sinkOptions: sinkOpts,
                    columnOptions: columnOpts)
                .CreateLogger();

            services.AddSingleton(Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StratagemDbContext stratagemDbContext, FactionDbContext factionDbContext)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JellyDev.WH40K.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            stratagemDbContext.Database.Migrate();
            factionDbContext.Database.Migrate();
        }
    }
}
