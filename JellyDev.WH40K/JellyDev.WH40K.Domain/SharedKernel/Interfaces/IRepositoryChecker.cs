

namespace JellyDev.WH40K.Domain.SharedKernel.Interfaces
{
    /// <summary>
    /// Interface for a repository checker to check if a record exists
    /// </summary>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public interface IRepositoryChecker<TId>
        where TId : class
    {
        /// <summary>
        /// Check for the existence of an aggregate
        /// </summary>
        /// <param name="id">ID of the aggregate</param>
        /// <returns>True if the aggregate exists</returns>
        bool Exists(TId id);
    }
}
