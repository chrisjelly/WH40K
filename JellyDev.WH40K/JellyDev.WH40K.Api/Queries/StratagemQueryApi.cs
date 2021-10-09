using JellyDev.WH40K.Api.SharedKernel;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.Queries
{
    [Route("api/v1/stratagem")]
    [ApiController]
    public class StratagemQueryApi : QueryApiBaseController
    {
        /// <summary>
        /// List Stratagems query service
        /// </summary>
        private readonly IAsyncQueryService<Infrastructure.Stratagem.ReadModels.Stratagem, Infrastructure.Stratagem.QueryModels.ListStratagems> _listStratagemSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listStratagemSvc">List Stratagems query service</param>
        public StratagemQueryApi(IAsyncQueryService<Infrastructure.Stratagem.ReadModels.Stratagem, Infrastructure.Stratagem.QueryModels.ListStratagems> listStratagemSvc)
        {
            _listStratagemSvc = listStratagemSvc;
        }

        /// <summary>
        /// Get a list of stratagems
        /// </summary>
        /// <param name="request">Query request</param>
        /// <returns>A list of stratagems</returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get([FromQuery] Infrastructure.Stratagem.QueryModels.ListStratagems request) => await ExecuteQueryAsync(request, _listStratagemSvc.QueryAsync);
    }
}
