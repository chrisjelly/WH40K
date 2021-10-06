using JellyDev.WH40K.Domain.SharedKernel;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Interface for adding new aggregates to a repository
    /// </summary>
    public interface IRepositoryCreator<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {
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
    }
}
