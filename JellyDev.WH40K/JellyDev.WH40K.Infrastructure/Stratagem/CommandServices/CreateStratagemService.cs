using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using MoreLinq;

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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryCreator">Stratagem repository creator</param>
        /// <param name="unitOfWork">Stratagem unit of work</param>
        public CreateStratagemService(IRepositoryCreator<StratagemAggregate, StratagemId> repositoryCreator, IUnitOfWork unitOfWork)
        {
            _repositoryCreator = repositoryCreator;
            _unitOfWork = unitOfWork;
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
                phases, 
                Name.FromString(command.Name), 
                Description.FromString(command.Description));

            var stratagem = new StratagemAggregate(createStratagemParams);

            await _repositoryCreator.AddAsync(stratagem);
            await _unitOfWork.CommitAsync();
        }
    }
}
