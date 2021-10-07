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
        /// Update Stratagem command service
        /// </summary>
        private readonly IAsyncCommandService<UpdateStratagem> _updateStratagemSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createStratagemSvc">Create Stratagem command service</param>
        /// <param name="updateStratagemSvc">Update Stratagem command service</param>
        public StratagemCommandApi(IAsyncCommandService<CreateStratagem> createStratagemSvc, IAsyncCommandService<UpdateStratagem> updateStratagemSvc)
        {
            _createStratagemSvc = createStratagemSvc;
            _updateStratagemSvc = updateStratagemSvc;
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

        /// <summary>
        /// Update a stratagem
        /// </summary>
        /// <param name="updateStratagemCmd">The command to update the stratagem</param>
        /// <returns>Task</returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync(UpdateStratagem updateStratagemCmd)
        {
            await _updateStratagemSvc.ExecuteAsync(updateStratagemCmd);
            return new OkResult();
        }
    }
}
