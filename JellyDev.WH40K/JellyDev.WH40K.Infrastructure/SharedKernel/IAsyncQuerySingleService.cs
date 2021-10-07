﻿using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Generic asynchronous query service interface for queries with a single result
    /// </summary>
    /// <typeparam name="TReadModel">Type of read model</typeparam>
    /// <typeparam name="TQueryModel">Type of query model</typeparam>
    public interface IAsyncQuerySingleService<TReadModel, TQueryModel>
        where TQueryModel : class
    {
        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        Task<TReadModel> QueryAsync(TQueryModel queryModel);
    }
}