using Autofac;
using JellyDev.WH40K.Domain.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.SharedKernel.Decorators;
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
                .AsClosedTypesOf(typeof(IRepositoryChecker<>))
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryCreator<,>))
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryUpdater<,>))
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryDeleter<,>))
                .InstancePerLifetimeScope();

            // Register command services
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncCommandService<>))
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandService<>))
                .InstancePerLifetimeScope();

            // Register async query services
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncQueryService<,>))
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncQuerySingleService<,>))
                .InstancePerLifetimeScope();

            // Register decorators
            builder.RegisterGenericDecorator(typeof(LoggingAsyncCommandService<>), typeof(IAsyncCommandService<>));
            builder.RegisterGenericDecorator(typeof(LoggingCommandService<>), typeof(ICommandService<>));
            builder.RegisterGenericDecorator(typeof(LoggingAsyncQueryService<,>), typeof(IAsyncQueryService<,>));
            builder.RegisterGenericDecorator(typeof(LoggingAsyncQuerySingleService<,>), typeof(IAsyncQuerySingleService<,>));
        }
    }
}
