using JellyDev.WH40K.Api.SharedKernel;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Api.Commands
{
    [Route("api/v1/faction")]
    [ApiController]
    public class FactionCommandApi : CommandApiBaseController
    {
        /// <summary>
        /// Create Faction command service
        /// </summary>
        private readonly IAsyncCommandService<CreateFaction> _createFactionSvc;

        /// <summary>
        /// Update Faction command service
        /// </summary>
        private readonly IAsyncCommandService<UpdateFaction> _updateFactionSvc;

        /// <summary>
        /// Delete Faction command service
        /// </summary>
        private readonly IAsyncCommandService<DeleteFaction> _deleteFactionSvc;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createFactionSvc">Create Faction command service</param>
        /// <param name="updateFactionSvc">Update Faction command service</param>
        /// <param name="deleteFactionSvc">Delete Faction command service</param>
        public FactionCommandApi(IAsyncCommandService<CreateFaction> createFactionSvc, IAsyncCommandService<UpdateFaction> updateFactionSvc,
            IAsyncCommandService<DeleteFaction> deleteFactionSvc)
        {
            _createFactionSvc = createFactionSvc;
            _updateFactionSvc = updateFactionSvc;
            _deleteFactionSvc = deleteFactionSvc;
        }

        /// <summary>
        /// Create a faction
        /// </summary>
        /// <param name="createFactionCmd">The command to create the faction</param>
        /// <returns>API command result</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateFaction createFactionCmd) => await ExecuteCommandAsync(createFactionCmd, _createFactionSvc.ExecuteAsync);

        /// <summary>
        /// Update a faction
        /// </summary>
        /// <param name="updateFactionCmd">The command to update the faction</param>
        /// <returns>API command result</returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync(UpdateFaction updateFactionCmd) => await ExecuteCommandAsync(updateFactionCmd, _updateFactionSvc.ExecuteAsync);

        /// <summary>
        /// Delete a faction
        /// </summary>
        /// <param name="deleteFactionCmd">The command to delete the faction</param>
        /// <returns>API command result</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DeleteFaction deleteFactionCmd) => await ExecuteCommandAsync(deleteFactionCmd, _deleteFactionSvc.ExecuteAsync);
    }
}
