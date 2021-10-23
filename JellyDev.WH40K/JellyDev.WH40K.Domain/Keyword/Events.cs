using System;

namespace JellyDev.WH40K.Domain.Keyword
{
    /// <summary>
    /// Keyword event declarations
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Keyword created
        /// </summary>
        public class KeywordCreated
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// ID of the faction owning this stratagem
            /// </summary>
            public Guid FactionId { get; set; }

            /// <summary>
            /// Name of the keyword
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Keyword updated
        /// </summary>
        public class KeywordUpdated
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// ID of the faction owning this stratagem
            /// </summary>
            public Guid FactionId { get; set; }

            /// <summary>
            /// Name of the keyword
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Keyword deleted
        /// </summary>
        public class KeywordDeleted
        {
            /// <summary>
            /// ID
            /// </summary>
            public Guid Id { get; set; }
        }
    }
}
