using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Decorators
{
    /// <summary>
    /// Async Query Single Service decorator to log the execution somewhere
    /// </summary>
    public class LoggingAsyncQuerySingleService<TReadModel, TQueryModel> : IAsyncQuerySingleService<TReadModel, TQueryModel>
        where TQueryModel : class
    {
        /// <summary>
        /// The decorated Query Single Service
        /// </summary>
        private readonly IAsyncQuerySingleService<TReadModel, TQueryModel> _decoratee;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Query Single Service</param>
        public LoggingAsyncQuerySingleService(IAsyncQuerySingleService<TReadModel, TQueryModel> decoratee)
        {
            _decoratee = decoratee;
        }

        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        public async Task<TReadModel> QueryAsync(TQueryModel queryModel)
        {
            TReadModel result = await _decoratee.QueryAsync(queryModel);
            // TODO: Log something here

            return result;
        }
    }
}
