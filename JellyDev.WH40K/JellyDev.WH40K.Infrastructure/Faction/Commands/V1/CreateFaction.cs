using System;

namespace JellyDev.WH40K.Infrastructure.Faction.Commands.V1
{
    /// <summary>
    /// Create Faction command
    /// </summary>
    public class CreateFaction
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the faction
        /// </summary>
        public string Name { get; set; }
    }
}
