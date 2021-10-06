using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.Commands
{
    [Route("api/v1/stratagem")]
    [ApiController]
    public class StratagemCommandApi : ControllerBase
    {
        /// <summary>
        /// Create Stratagem command service
        /// </summary>
        private readonly IAsyncCommandService<CreateStratagem> _createStratagemSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createStratagemSvc">Create Stratagem command service</param>
        public StratagemCommandApi(IAsyncCommandService<CreateStratagem> createStratagemSvc)
        {
            _createStratagemSvc = createStratagemSvc;
        }

        /// <summary>
        /// Create a stratagem
        /// </summary>
        /// <param name="createStratagemCmd">The command to create the stratagem</param>
        /// <returns>Task</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateStratagem createStratagemCmd)
        {
            await _createStratagemSvc.ExecuteAsync(createStratagemCmd);
            return new OkResult();
        }
    }
}
