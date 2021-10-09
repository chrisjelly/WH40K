using JellyDev.WH40K.Api.SharedKernel;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.Queries
{
    [Route("api/v1/faction")]
    [ApiController]
    public class FactionQueryApi : QueryApiBaseController
    {
        /// <summary>
        /// List Factions query service
        /// </summary>
        private readonly IAsyncQueryService<Infrastructure.Faction.ReadModels.Faction, Infrastructure.Faction.QueryModels.ListFactions> _listFactionsSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listFactionsSvc">List Factions query service</param>
        public FactionQueryApi(IAsyncQueryService<Infrastructure.Faction.ReadModels.Faction, Infrastructure.Faction.QueryModels.ListFactions> listFactionsSvc)
        {
            _listFactionsSvc = listFactionsSvc;
        }

        /// <summary>
        /// Get a list of factions
        /// </summary>
        /// <param name="request">Query request</param>
        /// <returns>A list of factions</returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get([FromQuery] Infrastructure.Faction.QueryModels.ListFactions request) => await ExecuteQueryAsync(request, _listFactionsSvc.QueryAsync);
    }
}
