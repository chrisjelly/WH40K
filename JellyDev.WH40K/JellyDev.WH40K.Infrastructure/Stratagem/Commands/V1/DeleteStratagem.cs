using System;

namespace JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1
{
    /// <summary>
    /// Delete Stratagem command
    /// </summary>
    public class DeleteStratagem
    {
        /// <summary>
        /// ID of the stratagem to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
