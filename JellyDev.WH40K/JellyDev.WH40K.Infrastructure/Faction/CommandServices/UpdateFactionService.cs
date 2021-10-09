using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.Faction.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using System;
using System.Threading.Tasks;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;

namespace JellyDev.WH40K.Infrastructure.Faction.CommandServices
{
    /// <summary>
    /// Update Faction command service
    /// </summary>
    public class UpdateFactionService : IAsyncCommandService<UpdateFaction>
    {
        /// <summary>
        /// Faction repository updater
        /// </summary>
        private readonly IRepositoryUpdater<FactionAggregate, FactionId> _repositoryUpdater;

        /// <summary>
        /// Faction unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryUpdater">Faction repository updater</param>
        /// <param name="unitOfWork">Faction unit of work</param>
        public UpdateFactionService(IRepositoryUpdater<FactionAggregate, FactionId> repositoryUpdater, IUnitOfWork unitOfWork)
        {
            _repositoryUpdater = repositoryUpdater;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(UpdateFaction command)
        {
            var faction = _repositoryUpdater.Load(new FactionId(command.Id));
            if (faction == null) throw new InvalidOperationException($"Faction with id {command.Id} cannot be found");

            var updateFactionParams = new UpdateFactionParams(Name.FromString(command.Name));

            faction.Update(updateFactionParams);

            _repositoryUpdater.Update(faction);
            await _unitOfWork.CommitAsync();
        }
    }
}
