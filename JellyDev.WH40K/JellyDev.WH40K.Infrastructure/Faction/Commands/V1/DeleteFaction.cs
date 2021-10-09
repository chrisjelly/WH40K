using System;

namespace JellyDev.WH40K.Infrastructure.Faction.Commands.V1
{
    /// <summary>
    /// Delete Faction command
    /// </summary>
    public class DeleteFaction
    {
        /// <summary>
        /// ID of the faction to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
