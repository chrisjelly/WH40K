﻿using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Newtonsoft.Json;
using Serilog;
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
        /// Logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Async QueryService</param>
        /// <param name="logger">Logger</param>
        public LoggingAsyncQueryService(IAsyncQueryService<TReadModel, TQueryModel> decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        public async Task<IEnumerable<TReadModel>> QueryAsync(TQueryModel queryModel)
        {
            IEnumerable<TReadModel> results = await _decoratee.QueryAsync(queryModel);
            string serializedQuery = JsonConvert.SerializeObject(queryModel);
            _logger.ForContext("RequestType", "async query")
                .Information($"Executed async query: {serializedQuery}");
            return results;
        }
    }
}
