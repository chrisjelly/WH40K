using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Generic asynchronous command service interface
    /// </summary>
    /// <typeparam name="TCommand">Type of command to execute</typeparam>
    public interface IAsyncCommandService<TCommand>
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        Task ExecuteAsync(TCommand command);
    }
}
