using JellyDev.WH40K.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Stratagem
{
    /// <summary>
    /// Stratagem commands
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Version 1
        /// </summary>
        public static class V1
        {
            /// <summary>
            /// Create stratagem
            /// </summary>
            public class CreateStratagem
            {
                /// <summary>
                /// ID
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// The phases relevant to the stratagem
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
        }
    }
}
