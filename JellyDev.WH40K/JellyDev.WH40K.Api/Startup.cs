using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem;
using JellyDev.WH40K.Infrastructure.Stratagem.CommandServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using Microsoft.EntityFrameworkCore;

namespace JellyDev.WH40K.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JellyDev.WH40K.Api", Version = "v1" });
            });

            // Database
            string connectionString = Configuration.GetConnectionString("JellyDev");
            services.AddDbContextPool<StratagemDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(connectionString);
            });

            // Composition root
            services.AddScoped<IAsyncCommandService<CreateStratagem>, CreateStratagemService>();
            services.AddScoped<IRepository<StratagemAggregate, StratagemId>, StratagemRepository>();
            services.AddScoped<IUnitOfWork, StratagemUnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        }
    }
}
