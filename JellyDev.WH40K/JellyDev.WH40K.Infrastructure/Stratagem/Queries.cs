using Dapper;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Stratagem
{
    /// <summary>
    /// Stratagem queries
    /// </summary>
    public static class Queries
    {
        /// <summary>
        /// Get a list of stratagems
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="query">Query model</param>
        /// <returns>A list of stratagems</returns>
        public static Task<IEnumerable<ReadModels.Stratagem>> QueryAsync(
            this DbConnection connection,
            QueryModels.ListStratagems query)
            => connection.QueryAsync<ReadModels.Stratagem>(
                "SELECT s.\"Id\", s.\"Name\",  s.\"Description\" " +
                "FROM WH.\"Stratagems\" s " +
                "ORDER BY s.\"Id\" " +
                "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                new
                {
                    PageSize = query.PageSize,
                    Offset = QueriesHelper.Offset(query.Page, query.PageSize)
                });
    }
}
