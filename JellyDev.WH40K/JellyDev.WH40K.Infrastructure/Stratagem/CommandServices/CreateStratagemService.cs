using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoreLinq;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.Interfaces;

namespace JellyDev.WH40K.Infrastructure.Stratagem.CommandServices
{
    /// <summary>
    /// Create Stratagem command service
    /// </summary>
    public class CreateStratagemService : IAsyncCommandService<CreateStratagem>
    {
        /// <summary>
        /// Stratagem repository creator
        /// </summary>
        private readonly IRepositoryCreator<StratagemAggregate, StratagemId> _repositoryCreator;

        /// <summary>
        /// Stratagem unit of work
        /// </summary>
        private readonly IUnitOfWork<StratagemDbContext> _unitOfWork;

        /// <summary>
        /// Faction checker for confirming factions exist
        /// </summary>
        private readonly IRepositoryChecker<FactionId> _factionChecker;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryCreator">Stratagem repository creator</param>
        /// <param name="unitOfWork">Stratagem unit of work</param>
        /// <param name="factionChecker">Faction checker for confirming factions exist</param>
        public CreateStratagemService(IRepositoryCreator<StratagemAggregate, StratagemId> repositoryCreator, IUnitOfWork<StratagemDbContext> unitOfWork, IRepositoryChecker<FactionId> factionChecker)
        {
            _repositoryCreator = repositoryCreator;
            _unitOfWork = unitOfWork;
            _factionChecker = factionChecker;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(CreateStratagem command)
        {
            if (command.Id == Guid.Empty) command.Id = Guid.NewGuid();
            if (_repositoryCreator.Exists(command.Id.ToString())) throw new InvalidOperationException($"Stratagem with id {command.Id} already exists");

            var phases = new List<Phase>();
            command.Phases.ForEach(x => phases.Add(Phase.FromEnum(x)));

            var createStratagemParams = new CreateStratagemParams(new StratagemId(command.Id), 
                new FactionId(command.FactionId),
                phases, 
                Name.FromString(command.Name), 
                Description.FromString(command.Description),
                Amount.FromInt(command.CommandPoints));

            var stratagem = new StratagemAggregate(createStratagemParams, _factionChecker);

            await _repositoryCreator.AddAsync(stratagem);
            await _unitOfWork.CommitAsync();
        }
    }
}
