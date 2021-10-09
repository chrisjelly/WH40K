using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;

namespace JellyDev.WH40K.Domain.Faction.ParameterObjects
{
    /// <summary>
    /// Parameter object for updating a faction
    /// </summary>
    public class UpdateFactionParams 
    {
        /// <summary>
        /// Name of the faction
        /// </summary>
        public readonly Name Name;

        /// <summary>
        /// Create the parameter object
        /// </summary>
        /// <param name="name">Name of the faction</param>
        public UpdateFactionParams(Name name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
        }
    }
}
