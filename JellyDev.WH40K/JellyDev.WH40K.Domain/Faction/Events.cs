using System;

namespace JellyDev.WH40K.Domain.Faction
{
    /// <summary>
    /// Faction event declarations
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Faction created
        /// </summary>
        public class FactionCreated
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of the stratagem
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Faction updated
        /// </summary>
        public class FactionUpdated
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of the stratagem
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Faction deleted
        /// </summary>
        public class FactionDeleted
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }
        }
    }
}
