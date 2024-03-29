﻿using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoreLinq;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using JellyDev.WH40K.Domain.SharedKernel.Interfaces;
using JellyDev.WH40K.Domain.Faction;

namespace JellyDev.WH40K.Infrastructure.Stratagem.CommandServices
{
    /// <summary>
    /// Update Stratagem command service
    /// </summary>
    public class UpdateStratagemService : IAsyncCommandService<UpdateStratagem>
    {
        /// <summary>
        /// Stratagem repository updater
        /// </summary>
        private readonly IRepositoryUpdater<StratagemAggregate, StratagemId> _repositoryUpdater;

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
        /// <param name="repositoryUpdater">Stratagem repository updater</param>
        /// <param name="unitOfWork">Stratagem unit of work</param>
        /// <param name="factionChecker">Faction checker for confirming factions exist</param>
        public UpdateStratagemService(IRepositoryUpdater<StratagemAggregate, StratagemId> repositoryUpdater, IUnitOfWork<StratagemDbContext> unitOfWork, IRepositoryChecker<FactionId> factionChecker)
        {
            _repositoryUpdater = repositoryUpdater;
            _unitOfWork = unitOfWork;
            _factionChecker = factionChecker;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(UpdateStratagem command)
        {
            var stratagem = _repositoryUpdater.Load(new StratagemId(command.Id));
            if (stratagem == null) throw new InvalidOperationException($"Stratagem with id {command.Id} cannot be found");

            var phases = new List<Phase>();
            command.Phases.ForEach(x => phases.Add(Phase.FromEnum(x)));

            var updateStratagemParams = new UpdateStratagemParams(new FactionId(command.FactionId),
                phases,
                Name.FromString(command.Name),
                Description.FromString(command.Description),
                Amount.FromInt(command.CommandPoints));

            stratagem.Update(updateStratagemParams, _factionChecker);

            _repositoryUpdater.Update(stratagem);
            await _unitOfWork.CommitAsync();
        }
    }
}
