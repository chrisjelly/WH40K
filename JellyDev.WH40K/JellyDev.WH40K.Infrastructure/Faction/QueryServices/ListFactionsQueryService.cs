using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;

namespace JellyDev.WH40K.Infrastructure.Faction.QueryServices
{
    /// <summary>
    /// List Factions query service
    /// </summary>
    public class ListFactionsQueryService : IAsyncQueryService<ReadModels.Faction, QueryModels.ListFactions>
    {
        /// <summary>
        /// Database connection
        /// </summary>
        private readonly DbConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">Database connection</param>
        public ListFactionsQueryService(DbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Run the query
        /// </summary>
        /// <param name="queryModel">Query model</param>
        /// <returns>Read model results</returns>
        public async Task<IEnumerable<ReadModels.Faction>> QueryAsync(QueryModels.ListFactions queryModel) => await _connection.QueryAsync(queryModel);
    }
}
