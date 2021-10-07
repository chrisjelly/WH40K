using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoreLinq;

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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryUpdater">Stratagem repository updater</param>
        /// <param name="unitOfWork">Stratagem unit of work</param>
        public UpdateStratagemService(IRepositoryUpdater<StratagemAggregate, StratagemId> repositoryUpdater, IUnitOfWork unitOfWork)
        {
            _repositoryUpdater = repositoryUpdater;
            _unitOfWork = unitOfWork;
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
            var name = Name.FromString(command.Name);
            var description = Description.FromString(command.Description);
            var updateStratagemParams = new UpdateStratagemParams(phases, name, description);

            stratagem.Update(updateStratagemParams);

            _repositoryUpdater.Update(stratagem);
            await _unitOfWork.CommitAsync();
        }
    }
}
