using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Decorators
{
    /// <summary>
    /// Async Query Service decorator to log the execution somewhere
    /// </summary>
    public class LoggingAsyncQueryService<TReadModel, TQueryModel> : IAsyncQueryService<TReadModel, TQueryModel>
        where TQueryModel : class
    {
        /// <summary>
        /// The decorated Async QueryService
        /// </summary>
        private readonly IAsyncQueryService<TReadModel, TQueryModel> _decoratee;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Async QueryService</param>
        public LoggingAsyncQueryService(IAsyncQueryService<TReadModel, TQueryModel> decoratee)
        {
            _decoratee = decoratee;
        }

        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        public async Task<IEnumerable<TReadModel>> QueryAsync(TQueryModel queryModel)
        {
            IEnumerable<TReadModel> results = await _decoratee.QueryAsync(queryModel);
            // TODO: Log something here

            return results;
        }
    }
}
