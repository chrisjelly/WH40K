

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces
{
    /// <summary>
    /// Generic command service interface
    /// </summary>
    /// <typeparam name="TCommand">Type of command to execute</typeparam>
    public interface ICommandService<TCommand>
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        void Execute(TCommand command);
    }
}
