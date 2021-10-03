using JellyDev.WH40K.Infrastructure.SharedKernel;
using static JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;

namespace JellyDev.WH40K.Infrastructure.Stratagem.CommandServices
{
    /// <summary>
    /// Create Stratagem command service
    /// </summary>
    public class CreateStratagemService : ICommandService<CreateStratagem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CreateStratagemService()
        {
            // TODO: Dependencies
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public void Execute(CreateStratagem command)
        {
            throw new System.NotImplementedException();
        }
    }
}
