using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;

namespace JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1
{
    /// <summary>
    /// Update Stratagem command
    /// </summary>
    public class UpdateStratagem
    {
        /// <summary>
        /// ID of the stratagem to update
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the faction owning this stratagem
        /// </summary>
        public Guid FactionId { get; set; }

        /// <summary>
        /// The phases relevant to the stratagem
        /// </summary>
        public ICollection<PhaseEnum> Phases { get; set; }

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
        public int CommandPoints { get; set; }
    }
}
