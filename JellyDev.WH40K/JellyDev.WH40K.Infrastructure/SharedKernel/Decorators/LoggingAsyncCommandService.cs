using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Decorators
{
    /// <summary>
    /// Async Command Service decorator to log the execution somewhere
    /// </summary>
    public class LoggingAsyncCommandService<TCommand> : IAsyncCommandService<TCommand>
    {
        /// <summary>
        /// The decorated Async Command Service
        /// </summary>
        private readonly IAsyncCommandService<TCommand> _decoratee;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Async Command Service</param>
        public LoggingAsyncCommandService(IAsyncCommandService<TCommand> decoratee)
        {
            _decoratee = decoratee;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(TCommand command)
        {            
            await _decoratee.ExecuteAsync(command);
            // TODO: Log something here
        }
    }
}
