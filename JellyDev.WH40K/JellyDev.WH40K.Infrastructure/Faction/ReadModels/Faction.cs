using System;

namespace JellyDev.WH40K.Infrastructure.Faction.ReadModels
{
    /// <summary>
    /// Faction read model
    /// </summary>
    public class Faction
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
