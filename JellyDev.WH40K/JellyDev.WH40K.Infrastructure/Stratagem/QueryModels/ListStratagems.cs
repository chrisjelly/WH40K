using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using System;

namespace JellyDev.WH40K.Infrastructure.Stratagem.QueryModels
{
    /// <summary>
    /// Query model to get a list of stratagems
    /// </summary>
    public class ListStratagems : ListQueryModel
    { 
        /// <summary>
        /// Look up stratagems for this faction
        /// </summary>
        public Guid FactionId { get; set; }

        /// <summary>
        /// Look up stratagems for this phase
        /// </summary>
        public PhaseEnum Phase { get; set; }
    }
}
