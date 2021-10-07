using JellyDev.WH40K.Domain.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Interface for updating aggregates in a repository
    /// </summary>
    /// <typeparam name="TAggregate">Type of aggregate stored in the repository</typeparam>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public interface IRepositoryUpdater<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {
        /// <summary>
        /// Load an aggregate
        /// </summary>
        /// <param name="id">ID of the issuer environment</param>
        /// <returns>The requested issuer environment</returns>
        TAggregate Load(TId id);

        /// <summary>
        /// Update an aggregate
        /// </summary>
        /// <param name="entity">The updated aggregate</param>
        void Update(TAggregate entity);
    }
}
