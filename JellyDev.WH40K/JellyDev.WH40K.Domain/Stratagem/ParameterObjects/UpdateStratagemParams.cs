using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;

namespace JellyDev.WH40K.Domain.Stratagem.ParameterObjects
{
    /// <summary>
    /// Parameter object for updating a stratagem
    /// </summary>
    public class UpdateStratagemParams
    {
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
        /// <param name="factionId">ID of the faction owning this stratagem</param>
        /// <param name="phases">The phases relevant to the stratagem</param>
        /// <param name="name">Name of the stratagem</param>
        /// <param name="description">Description of the stratagem</param>
        /// <param name="commandPoints">The command points cost for the stratagem</param>
        public UpdateStratagemParams(FactionId factionId, ICollection<Phase> phases, Name name, Description description, Amount commandPoints)
        {
            if (factionId == null) throw new ArgumentNullException(nameof(factionId));
            if (phases == null) throw new ArgumentNullException(nameof(phases));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (commandPoints == null) throw new ArgumentNullException(nameof(commandPoints));

            FactionId = factionId;
            Phases = phases;
            Name = name;
            Description = description;
            CommandPoints = commandPoints;
        }
    }
}
