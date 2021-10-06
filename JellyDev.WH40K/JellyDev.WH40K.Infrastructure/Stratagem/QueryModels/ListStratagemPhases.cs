using System;

namespace JellyDev.WH40K.Infrastructure.Stratagem.QueryModels
{
    /// <summary>
    /// Query model to get a list of phases for a stratagem
    /// </summary>
    public class ListStratagemPhases
    {
        /// <summary>
        /// ID of the stratagem
        /// </summary>
        public Guid StratagemId { get; set; }
    }
}
