using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;
using JellyDev.WH40K.Infrastructure.Database.EfCore;

namespace JellyDev.WH40K.Infrastructure.Faction.CommandServices
{
    /// <summary>
    /// Delete Faction command service
    /// </summary>
    public class DeleteFactionService : IAsyncCommandService<DeleteFaction>
    {
        /// <summary>
        /// Faction repository deleter
        /// </summary>
        private readonly IRepositoryDeleter<FactionAggregate, FactionId> _repositoryDeleter;

        /// <summary>
        /// Faction unit of work
        /// </summary>
        private readonly IUnitOfWork<FactionDbContext> _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryDeleter">Faction repository deleter</param>
        /// <param name="unitOfWork">Stratagem unit of work</param>
        public DeleteFactionService(IRepositoryDeleter<FactionAggregate, FactionId> repositoryDeleter, IUnitOfWork<FactionDbContext> unitOfWork)
        {
            _repositoryDeleter = repositoryDeleter;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(DeleteFaction command)
        {
            var faction = _repositoryDeleter.Load(new FactionId(command.Id));
            if (faction == null) throw new InvalidOperationException($"Faction with id {command.Id} cannot be found");

            faction.Delete();

            _repositoryDeleter.Delete(faction);
            await _unitOfWork.CommitAsync();
        }
    }
}
