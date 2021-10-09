using System.Collections.Generic;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces
{
    /// <summary>
    /// Generic asynchronous query service interface for queries with multiple results
    /// </summary>
    /// <typeparam name="TReadModel">Type of read model</typeparam>
    /// <typeparam name="TQueryModel">Type of query model</typeparam>
    public interface IAsyncQueryService<TReadModel, TQueryModel>
        where TQueryModel : class
    {
        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        Task<IEnumerable<TReadModel>> QueryAsync(TQueryModel queryModel);
    }
}
