using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Linq;

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

            foreach(ReadModels.Stratagem stratagem in stratagems)
            {
                stratagem.Phases = await ListPhasesAsync(stratagem.Id);
            }

            return stratagems;
        }

        /// <summary>
        /// List the phases for a stratagem
        /// </summary>
        /// <param name="stratagemId">ID of the stratagem</param>
        /// <returns>Phases for the stratagem</returns>
        private async Task<PhaseEnum[]> ListPhasesAsync(Guid stratagemId)
        {
            var queryModel = new QueryModels.ListStratagemPhases
            {
                StratagemId = stratagemId
            };
            IEnumerable<PhaseEnum> phases = await _connection.QueryAsync(queryModel);
            return phases.ToArray();
        }
    }
}
