using Autofac;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace JellyDev.WH40K.Api
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            System.Reflection.Assembly assembly = typeof(CreateStratagem).Assembly;

            // Register database connection for Dapper queries
            builder.Register<DbConnection>(x => new SqlConnection(_connectionString));

            // Register unit of work
            builder.RegisterAssemblyTypes(assembly)
                .As<IUnitOfWork>();

            // Register repositories
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryCreator<,>));
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryUpdater<,>));

            // Register async command services
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncCommandService<>));

            // Register async query services
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncQueryService<,>));
        }
    }
}
