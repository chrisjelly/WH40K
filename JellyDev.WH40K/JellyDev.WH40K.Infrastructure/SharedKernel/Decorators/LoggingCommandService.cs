using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;

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
        /// Constructor
        /// </summary>
        /// <param name="decoratee">The decorated Command Service</param>
        public LoggingCommandService(ICommandService<TCommand> decoratee)
        {
            _decoratee = decoratee;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public void Execute(TCommand command)
        {
            _decoratee.Execute(command);
            // TODO: Log something here
        }
    }
}
