using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        /// Logger
        /// </summary>
        private readonly ILogger<LoggingAsyncCommandService<TCommand>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Async Command Service</param>
        /// <param name="logger">Logger</param>
        public LoggingAsyncCommandService(IAsyncCommandService<TCommand> decoratee, ILogger<LoggingAsyncCommandService<TCommand>> logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public async Task ExecuteAsync(TCommand command)
        {            
            await _decoratee.ExecuteAsync(command);
            string serializedCommand = JsonConvert.SerializeObject(command);
            _logger.LogInformation($"Executed async command: {serializedCommand}");
        }
    }
}
