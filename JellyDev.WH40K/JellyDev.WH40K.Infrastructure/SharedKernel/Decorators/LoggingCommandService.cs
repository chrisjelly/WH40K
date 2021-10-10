using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Decorators
{
    /// <summary>
    /// Command Service decorator to log the execution somewhere
    /// </summary>
    public class LoggingCommandService<TCommand> : ICommandService<TCommand>
    {
        /// <summary>
        /// The decorated Command Service
        /// </summary>
        private readonly ICommandService<TCommand> _decoratee;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<LoggingCommandService<TCommand>> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Command Service</param>
        /// <param name="logger">Logger</param>
        public LoggingCommandService(ICommandService<TCommand> decoratee, ILogger<LoggingCommandService<TCommand>> logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public void Execute(TCommand command)
        {
            _decoratee.Execute(command);
            string serializedCommand = JsonConvert.SerializeObject(command);
            _logger.LogInformation($"Executed command: {serializedCommand}");
        }
    }
}
