using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Newtonsoft.Json;
using Serilog;
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
        /// Logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Query Single Service</param>
        /// <param name="logger">Logger</param>
        public LoggingAsyncQuerySingleService(IAsyncQuerySingleService<TReadModel, TQueryModel> decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        public async Task<TReadModel> QueryAsync(TQueryModel queryModel)
        {
            TReadModel result = await _decoratee.QueryAsync(queryModel);
            string serializedQuery = JsonConvert.SerializeObject(queryModel);
            _logger.ForContext("RequestType", "async query single")
                .Information($"Executed async query single: {serializedQuery}");
            return result;
        }
    }
}
