using JellyDev.WH40K.Domain.SharedKernel;
using System;

namespace JellyDev.WH40K.Domain.Stratagem
{
    /// <summary>
    /// Stratagem event declarations
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Stratagem created
        /// </summary>
        public class StratagemCreated
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// The phases relevant to this stratagem
            /// </summary>
            public Phase[] Phases { get; set; }

            /// <summary>
            /// Name of the stratagem
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Description of the stratagem
            /// </summary>
            public string Description { get; set; }
        }

        /// <summary>
        /// Stratagem updated
        /// </summary>
        public class StratagemUpdated
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// The phases relevant to this stratagem
            /// </summary>
            public Phase[] Phases { get; set; }

            /// <summary>
            /// Name of the stratagem
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Description of the stratagem
            /// </summary>
            public string Description { get; set; }
        }

        /// <summary>
        /// Stratagem deleted
        /// </summary>
        public class StratagemDeleted
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }
        }
    }
}
