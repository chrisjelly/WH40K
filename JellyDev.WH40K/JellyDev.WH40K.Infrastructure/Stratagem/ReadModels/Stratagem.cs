using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;

namespace JellyDev.WH40K.Infrastructure.Stratagem.ReadModels
{
    /// <summary>
    /// Stratagem read model
    /// </summary>
    public class Stratagem
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The phases relevant to the stratagem
        /// </summary>
        public PhaseEnum[] Phases { get; set; }

        /// <summary>
        /// Name of the stratagem
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the stratagem
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The command points cost for the stratagem
        /// </summary>
        public string CommandPoints { get; set; }
    }
}
