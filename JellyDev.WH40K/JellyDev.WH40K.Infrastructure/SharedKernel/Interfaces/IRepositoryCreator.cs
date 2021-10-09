using JellyDev.WH40K.Domain.SharedKernel;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces
{
    /// <summary>
    /// Interface for adding new aggregates to a repository
    /// </summary>
    /// <typeparam name="TAggregate">Type of aggregate stored in the repository</typeparam>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public interface IRepositoryCreator<TAggregate, TId> : IRepositoryChecker<TId>
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {
        /// <summary>
        /// Add a new aggregate
        /// </summary>
        /// <param name="entity">The new aggregate</param>
        /// <returns>Task</returns>
        Task AddAsync(TAggregate entity);
    }
}
