﻿using JellyDev.WH40K.Domain.SharedKernel;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;

namespace JellyDev.WH40K.Domain.Stratagem.ParameterObjects
{
    /// <summary>
    /// Parameter object for creating a stratagem
    /// </summary>
    public class CreateStratagem
    {
        /// <summary>
        /// Unique ID of the stratagem
        /// </summary>
        public readonly StratagemId Id;

        /// <summary>
        /// The phases relevant to the stratagem
        /// </summary>
        public readonly Phase[] Phases;

        /// <summary>
        /// Name of the stratagem
        /// </summary>
        public readonly Name Name;

        /// <summary>
        /// Description of the stratagem
        /// </summary>
        public readonly Description Description;

        /// <summary>
        /// Create the parameter object
        /// </summary>
        /// <param name="id">Unique ID of the stratagem</param>
        /// <param name="phases">The phases relevant to the stratagem</param>
        /// <param name="name">Name of the stratagem</param>
        /// <param name="description">Description of the stratagem</param>
        public CreateStratagem(StratagemId id, Phase[] phases, Name name, Description description)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (phases == null) throw new ArgumentNullException(nameof(phases));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (description == null) throw new ArgumentNullException(nameof(description));

            Id = id;
            Phases = phases;
            Name = name;
            Description = description;
        }
    }
}
