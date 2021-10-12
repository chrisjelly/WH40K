using Dapper;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
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
                "SELECT s.\"Id\", s.\"FactionId\", s.\"Name\",  s.\"Description\", s.\"CommandPoints\" " +
                "FROM WH.\"Stratagems\" s " +
                "INNER JOIN WH.\"Stratagem_Phases\" sp ON s.\"Id\" = sp.\"StratagemId\" " +
                "WHERE (s.\"FactionId\" = @FactionId OR s.\"FactionId\" = 0x0) " +
                $"AND (sp.\"Phase\" = @Phase OR @Phase = {(int)PhaseEnum.Any}) " +
                "ORDER BY s.\"Id\" " +
                "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY",
                new
                {
                    FactionId = query.FactionId,
                    Phase = query.Phase,
                    PageSize = query.PageSize,
                    Offset = QueriesHelper.Offset(query.Page, query.PageSize)
                });

        /// <summary>
        /// Get a list of phases for a stratagem
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="query">Query model</param>
        /// <returns>A list of phases for a stratagem</returns>
        public static Task<IEnumerable<PhaseEnum>> QueryAsync(
            this DbConnection connection,
            QueryModels.ListStratagemPhases query)
            => connection.QueryAsync<PhaseEnum>(
                "SELECT \"Phase\" " +
                "FROM WH.\"Stratagem_Phases\" " +
                "WHERE StratagemId = @StratagemId " +
                "ORDER BY \"Phase\"",
                new
                {
                    StratagemId = query.StratagemId
                });            
    }
}
