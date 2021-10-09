using JellyDev.WH40K.Domain.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces
{
    public interface IRepositoryLoader<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {
        /// <summary>
        /// Load an aggregate
        /// </summary>
        /// <param name="id">ID of the aggregate</param>
        /// <returns>The requested aggregate</returns>
        TAggregate Load(TId id);
    }
}
