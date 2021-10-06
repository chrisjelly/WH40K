using JellyDev.WH40K.Domain.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Repository interface for an aggregate
    /// </summary>
    /// <typeparam name="TAggregate">Type of aggregate</typeparam>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public interface IRepository<TAggregate, TId> : IRepositoryCreator<TAggregate, TId>, IRepositoryUpdater<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {

    }
}
