using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;

namespace JellyDev.WH40K.Domain.Keyword.ParameterObjects
{
    /// <summary>
    /// Parameter object for creating a keyword
    /// </summary>
    public class CreateKeywordParams
    {
        /// <summary>
        /// Unique ID of the keyword
        /// </summary>
        public readonly KeywordId Id;

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
        /// <param name="id">Unique ID of the keyword</param>
        /// <param name="factionId">ID of the faction owning this keyword</param>
        /// <param name="name">Name of the keyword</param>
        public CreateKeywordParams(KeywordId id, FactionId factionId, Name name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (factionId == null) throw new ArgumentNullException(nameof(factionId));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Id = id;
            FactionId = factionId;
            Name = name;
        }
    }
}
