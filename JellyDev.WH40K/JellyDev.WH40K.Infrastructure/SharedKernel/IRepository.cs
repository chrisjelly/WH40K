using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Repository interface for an aggregate
    /// </summary>
    /// <typeparam name="TAggregate">Type of aggregate</typeparam>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public interface IRepository<TAggregate, TId>
    {
        /// <summary>
        /// Load an aggregate
        /// </summary>
        /// <param name="id">ID of the issuer environment</param>
        /// <returns>The requested issuer environment</returns>
        TAggregate Load(TId id);

        /// <summary>
        /// Add a new aggregate
        /// </summary>
        /// <param name="entity">The new aggregate</param>
        /// <returns>Task</returns>
        Task AddAsync(TAggregate entity);

        /// <summary>
        /// Check for the existence of an aggregate
        /// </summary>
        /// <param name="id">ID of the aggregate</param>
        /// <returns>True if the aggregate exists</returns>
        bool Exists(TId id);

        /// <summary>
        /// Update an aggregate
        /// </summary>
        /// <param name="entity">The updated aggregate</param>
        void Update(TAggregate entity);
    }
}
