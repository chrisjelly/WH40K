using Autofac;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using JellyDev.WH40K.Infrastructure.Stratagem.CommandServices;
using JellyDev.WH40K.Infrastructure.Stratagem.QueryServices;
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
            builder.Register<DbConnection>(x => new SqlConnection(_connectionString));

            builder.RegisterType<StratagemUnitOfWork>()
                .As<IUnitOfWork>();

            builder.RegisterType<StratagemRepository>()
                .As<IRepositoryCreator<StratagemAggregate, StratagemId>>();
            builder.RegisterType<StratagemRepository>()
                .As<IRepositoryUpdater<StratagemAggregate, StratagemId>>();

            builder.RegisterType<CreateStratagemService>()
                .As<IAsyncCommandService<CreateStratagem>>();
            builder.RegisterType<UpdateStratagemService>()
                .As<IAsyncCommandService<UpdateStratagem>>();

            builder.RegisterType<ListStratagemsService>()
                .As<IAsyncQueryService<Infrastructure.Stratagem.ReadModels.Stratagem, Infrastructure.Stratagem.QueryModels.ListStratagems>>();
        }
    }
}
