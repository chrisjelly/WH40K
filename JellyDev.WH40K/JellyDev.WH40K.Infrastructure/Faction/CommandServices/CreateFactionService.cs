using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.Faction.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;
using JellyDev.WH40K.Infrastructure.Database.EfCore;

namespace JellyDev.WH40K.Infrastructure.Faction.CommandServices
{
    /// <summary>
    /// Create Faction command service
    /// </summary>
    public class CreateFactionService : IAsyncCommandService<CreateFaction>
    {
        /// <summary>
        /// Faction repository creator
        /// </summary>
        private readonly IRepositoryCreator<FactionAggregate, FactionId> _repositoryCreator;

        /// <summary>
        /// Faction unit of work
        /// </summary>
        private readonly IUnitOfWork<FactionDbContext> _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryCreator">Faction repository creator</param>
        /// <param name="unitOfWork">Faction unit of work</param>
        public CreateFactionService(IRepositoryCreator<FactionAggregate, FactionId> repositoryCreator, IUnitOfWork<FactionDbContext> unitOfWork)
        {
            _repositoryCreator = repositoryCreator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(CreateFaction command)
        {
            if (command.Id == Guid.Empty) command.Id = Guid.NewGuid();
            if (_repositoryCreator.Exists(command.Id.ToString())) throw new InvalidOperationException($"Faction with id {command.Id} already exists");

            var createFactionParams = new CreateFactionParams(new FactionId(command.Id),
                Name.FromString(command.Name));

            var faction = new FactionAggregate(createFactionParams);

            await _repositoryCreator.AddAsync(faction);
            await _unitOfWork.CommitAsync();
        }
    }
}
