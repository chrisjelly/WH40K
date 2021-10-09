using JellyDev.WH40K.Api.SharedKernel;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.Commands
{
    [Route("api/v1/stratagem")]
    [ApiController]
    public class StratagemCommandApi : CommandApiBaseController
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
        /// Delete Stratagem command service
        /// </summary>
        private readonly IAsyncCommandService<DeleteStratagem> _deleteStratagemSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createStratagemSvc">Create Stratagem command service</param>
        /// <param name="updateStratagemSvc">Update Stratagem command service</param>
        /// <param name="deleteStratagemSvc">Delete Stratagem command service</param>
        public StratagemCommandApi(IAsyncCommandService<CreateStratagem> createStratagemSvc, IAsyncCommandService<UpdateStratagem> updateStratagemSvc, 
            IAsyncCommandService<DeleteStratagem> deleteStratagemSvc)
        {
            _createStratagemSvc = createStratagemSvc;
            _updateStratagemSvc = updateStratagemSvc;
            _deleteStratagemSvc = deleteStratagemSvc;
        }

        /// <summary>
        /// Create a stratagem
        /// </summary>
        /// <param name="createStratagemCmd">The command to create the stratagem</param>
        /// <returns>Task</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateStratagem createStratagemCmd) => await ExecuteCommandAsync(createStratagemCmd, _createStratagemSvc.ExecuteAsync);


        /// <summary>
        /// Update a stratagem
        /// </summary>
        /// <param name="updateStratagemCmd">The command to update the stratagem</param>
        /// <returns>Task</returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync(UpdateStratagem updateStratagemCmd) => await ExecuteCommandAsync(updateStratagemCmd, _updateStratagemSvc.ExecuteAsync);

        /// <summary>
        /// Delete a stratagem
        /// </summary>
        /// <param name="deleteStratagemCmd">The command to delete the stratagem</param>
        /// <returns>Task</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteStratagem deleteStratagemCmd) => await ExecuteCommandAsync(deleteStratagemCmd, _deleteStratagemSvc.ExecuteAsync);
    }
}
