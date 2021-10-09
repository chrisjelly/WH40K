using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;

namespace JellyDev.WH40K.Domain.Faction.ParameterObjects
{
    /// <summary>
    /// Parameter object for creating a faction
    /// </summary>
    public class CreateFactionParams
    {
        /// <summary>
        /// Unique ID of the faction
        /// </summary>
        public readonly FactionId Id;

        /// <summary>
        /// Name of the faction
        /// </summary>
        public readonly Name Name;

        /// <summary>
        /// Create the parameter object
        /// </summary>
        /// <param name="id">Unique ID of the faction</param>
        /// <param name="name">Name of the faction</param>
        public CreateFactionParams(FactionId id, Name name)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
        }
    }
}
