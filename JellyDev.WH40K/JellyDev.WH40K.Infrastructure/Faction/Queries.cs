using Dapper;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Faction
{
    /// <summary>
    /// Faction queries
    /// </summary>
    public static class Queries
    {
        /// <summary>
        /// Get a list of factions
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="query">Query model</param>
        /// <returns>A list of factions</returns>
        public static Task<IEnumerable<ReadModels.Faction>> QueryAsync(
            this DbConnection connection,
            QueryModels.ListFactions query)
            => connection.QueryAsync<ReadModels.Faction>(
                "SELECT s.\"Id\", s.\"Name\" " +
                "FROM WH.\"Factions\" f " +
                "ORDER BY f.\"Id\" " +
                "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                new
                {
                    PageSize = query.PageSize,
                    Offset = QueriesHelper.Offset(query.Page, query.PageSize)
                });
    }
}
