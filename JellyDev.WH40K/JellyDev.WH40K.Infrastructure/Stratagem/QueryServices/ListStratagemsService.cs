using JellyDev.WH40K.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Stratagem.QueryServices
{
    /// <summary>
    /// List Stratagems query service
    /// </summary>
    public class ListStratagemsService : IAsyncQueryService<ReadModels.Stratagem, QueryModels.ListStratagems>
    {
        /// <summary>
        /// Database connection
        /// </summary>
        private readonly DbConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">Database connection</param>
        public ListStratagemsService(DbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        public async Task<IEnumerable<ReadModels.Stratagem>> QueryAsync(QueryModels.ListStratagems queryModel)
        {
            IEnumerable<ReadModels.Stratagem> stratagems = await _connection.QueryAsync(queryModel);

            // TODO: Get the phases for each stratagem

            return stratagems;
        }
    }
}
