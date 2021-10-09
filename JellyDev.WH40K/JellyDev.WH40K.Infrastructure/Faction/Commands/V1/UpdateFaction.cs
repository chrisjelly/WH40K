using System;

namespace JellyDev.WH40K.Infrastructure.Faction.Commands.V1
{
    /// <summary>
    /// Update Faction command
    /// </summary>
    public class UpdateFaction
    {
        /// <summary>
        /// ID of the faction to update
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the faction
        /// </summary>
        public string Name { get; set; }
    }
}
