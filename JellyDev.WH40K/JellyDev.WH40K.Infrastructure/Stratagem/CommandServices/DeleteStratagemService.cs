using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;

namespace JellyDev.WH40K.Infrastructure.Stratagem.CommandServices
{
    /// <summary>
    /// Delete Stratagem command service
    /// </summary>
    public class DeleteStratagemService : IAsyncCommandService<DeleteStratagem>
    {
        /// <summary>
        /// Stratagem repository deleter
        /// </summary>
        private readonly IRepositoryDeleter<StratagemAggregate, StratagemId> _repositoryDeleter;

        /// <summary>
        /// Stratagem unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositoryDeleter">Stratagem repository deleter</param>
        /// <param name="unitOfWork">Stratagem unit of work</param>
        public DeleteStratagemService(IRepositoryDeleter<StratagemAggregate, StratagemId> repositoryDeleter, IUnitOfWork unitOfWork)
        {
            _repositoryDeleter = repositoryDeleter;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(DeleteStratagem command)
        {
            var stratagem = _repositoryDeleter.Load(new StratagemId(command.Id));
            if (stratagem == null) throw new InvalidOperationException($"Stratagem with id {command.Id} cannot be found");

            stratagem.Delete();

            _repositoryDeleter.Delete(stratagem);
            await _unitOfWork.CommitAsync();
        }
    }
}
