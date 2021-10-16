using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace JellyDev.WH40K.Domain.Stratagem.ParameterObjects
{
    /// <summary>
    /// Parameter object for creating a stratagem
    /// </summary>
    public class CreateStratagemParams
    {
        /// <summary>
        /// Unique ID of the stratagem
        /// </summary>
        public readonly StratagemId Id;

        /// <summary>
        /// ID of the faction owning this stratagem
        /// </summary>
        public FactionId FactionId { get; private set; }

        /// <summary>
        /// The phases relevant to the stratagem
        /// </summary>
        public readonly ICollection<Phase> Phases;

        /// <summary>
        /// Name of the stratagem
        /// </summary>
        public readonly Name Name;

        /// <summary>
        /// Description of the stratagem
        /// </summary>
        public readonly Description Description;

        /// <summary>
        /// The command points cost for the stratagem
        /// </summary>
        public readonly Amount CommandPoints;

        /// <summary>
        /// Create the parameter object
        /// </summary>
        /// <param name="id">Unique ID of the stratagem</param>
        /// <param name="factionId">ID of the faction owning this stratagem</param>
        /// <param name="phases">The phases relevant to the stratagem</param>
        /// <param name="name">Name of the stratagem</param>
        /// <param name="description">Description of the stratagem</param>
        /// <param name="commandPoints">The command points cost for the stratagem</param>
        public CreateStratagemParams(StratagemId id, FactionId factionId, ICollection<Phase> phases, Name name, Description description, Amount commandPoints)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (factionId == null) throw new ArgumentNullException(nameof(factionId));
            if (phases == null) throw new ArgumentNullException(nameof(phases));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (commandPoints == null) throw new ArgumentNullException(nameof(commandPoints));
           
            Id = id;
            FactionId = factionId;
            Phases = phases.DistinctBy(x => Phase.FromEnum(x.Value)).ToArray(); // Only select distinct phase values
            Name = name;
            Description = description;
            CommandPoints = commandPoints;
        }
    }
}
