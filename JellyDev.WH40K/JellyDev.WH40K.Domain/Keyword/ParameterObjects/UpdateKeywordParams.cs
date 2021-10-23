using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;

namespace JellyDev.WH40K.Domain.Keyword.ParameterObjects
{
    /// <summary>
    /// Parameter object for updating a keyword
    /// </summary>
    public class UpdateKeywordParams
    {
        /// <summary>
        /// ID of the faction owning this keyword
        /// </summary>
        public FactionId FactionId { get; private set; }

        /// <summary>
        /// Name of the keyword
        /// </summary>
        public readonly Name Name;

        /// <summary>
        /// Create the parameter object
        /// </summary>
        /// <param name="factionId">ID of the faction owning this keyword</param>
        /// <param name="name">Name of the keyword</param>
        public UpdateKeywordParams(FactionId factionId, Name name)
        {
            if (factionId == null) throw new ArgumentNullException(nameof(factionId));
            if (name == null) throw new ArgumentNullException(nameof(name));

            FactionId = factionId;
            Name = name;
        }
    }
}
