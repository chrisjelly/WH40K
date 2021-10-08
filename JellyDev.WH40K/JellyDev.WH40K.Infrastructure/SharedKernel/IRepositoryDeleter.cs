using JellyDev.WH40K.Domain.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Interface for deleting aggregates from a repository
    /// </summary>
    /// <typeparam name="TAggregate">Type of aggregate stored in the repository</typeparam>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public interface IRepositoryDeleter<TAggregate, TId> : IRepositoryLoader<TAggregate,TId>
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {
        /// <summary>
        /// Delete an aggregate
        /// </summary>
        /// <param name="entity">The aggregate to delete</param>
        /// <returns>Task</returns>
        void Delete(TAggregate entity);
    }
}
