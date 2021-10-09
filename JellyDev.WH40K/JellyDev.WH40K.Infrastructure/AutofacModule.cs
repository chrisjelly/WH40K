using Autofac;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;

namespace JellyDev.WH40K.Infrastructure
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            System.Reflection.Assembly assembly = typeof(CreateStratagem).Assembly;

            // Register units of work
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IUnitOfWork<>));

            // Register repositories
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryCreator<,>));
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryUpdater<,>));
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryDeleter<,>));

            // Register async command services
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncCommandService<>));

            // Register async query services
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncQueryService<,>));
        }
    }
}
