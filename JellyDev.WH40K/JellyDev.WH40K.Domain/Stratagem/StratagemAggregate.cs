﻿using JellyDev.WH40K.Domain.SharedKernel;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using System;
using static JellyDev.WH40K.Domain.SharedKernel.Exceptions;

namespace JellyDev.WH40K.Domain.Stratagem
{
    /// <summary>
    /// Stratagem aggregate root
    /// </summary>
    public class StratagemAggregate : AggregateRoot<StratagemId>
    {
        /// <summary>
        /// Protected constructor
        /// </summary>
        protected StratagemAggregate() { }

        /// <summary>
        /// The phases relevant to this stratagem
        /// </summary>
        public Phase[] Phases { get; private set; }

        /// <summary>
        /// Name of the stratagem
        /// </summary>
        public Name Name { get; private set;}

        /// <summary>
        /// Description of the stratagem
        /// </summary>
        public Description Description { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newStratagem">Parameter object for creating a stratagem</param>
        public StratagemAggregate(NewStratagem newStratagem)
        {
            if (newStratagem == null) throw new ArgumentNullException(nameof(newStratagem));

            Apply(new Events.StratagemCreated
            {
                Id = newStratagem.Id,
                Phases = newStratagem.Phases,
                Name = newStratagem.Name,
                Description = newStratagem.Description
            });
        }

        /// <summary>
        /// Update the stratagem
        /// </summary>
        /// <param name="changeStratagem">Parameter object for updating a stratagem</param>
        public void Update(ChangeStratagem changeStratagem)
        {
            if (changeStratagem == null) throw new ArgumentNullException(nameof(changeStratagem));

            Apply(new Events.StratagemUpdated
            {
                Id = Id,
                Phases = changeStratagem.Phases,
                Name = changeStratagem.Name,
                Description = changeStratagem.Description
            });
        }

        /// <summary>
        /// Delete the stratagem
        /// </summary>
        public void Delete()
        {
            Apply(new Events.StratagemDeleted
            {
                Id = Id
            });
        }

        /// <summary>
        /// Ensure the aggregate is in a valid state
        /// </summary>
        protected override void EnsureValidState()
        {
            bool valid =
                Id != null &&
                Phases != null && Phases.Length > 0 &&
                string.IsNullOrEmpty(Name) == false &&
                string.IsNullOrEmpty(Description) == false;

            if (!valid) throw new InvalidEntityStateException(this, $"Post-checks failed for stratagem");
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="event">The event to handle</param>
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.StratagemCreated e:
                    HandleStratagemCreated(e);
                    break;
                case Events.StratagemUpdated e:
                    HandleStratagemUpdated(e);
                    break;
            }
        }

        /// <summary>
        /// Stratagem Created event handler
        /// </summary>
        /// <param name="e">Stratagem Created event</param>
        private void HandleStratagemCreated(Events.StratagemCreated e)
        {
            Id = new StratagemId(e.Id);
            Phases = e.Phases;
            Name = Name.FromString(e.Name);
            Description = Description.FromString(e.Description);
        }

        /// <summary>
        /// Stratagem Updated event handler
        /// </summary>
        /// <param name="e">Stratagem Updated event</param>
        private void HandleStratagemUpdated(Events.StratagemUpdated e)
        {
            Phases = e.Phases;
            Name = Name.FromString(e.Name);
            Description = Description.FromString(e.Description);
        }
    }

    /// <summary>
    /// Stratagem ID
    /// </summary>
    public class StratagemId : Value<StratagemId>
    {
        /// <summary>
        /// Value
        /// </summary>
        public Guid Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected StratagemId() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value to use for the stratagem ID</param>
        public StratagemId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Stratagem id cannot be empty");
            Value = value;
        }

        /// <summary>
        /// Implicit operator to GUID
        /// </summary>
        /// <param name="self">Self reference</param>
        public static implicit operator Guid(StratagemId self)
        {
            if (self == null) return Guid.Empty;
            else return self.Value;
        }

        /// <summary>
        /// Implicit operator from string
        /// </summary>
        /// <param name="value">String value</param>
        public static implicit operator StratagemId(string value) => new StratagemId(Guid.Parse(value));

        /// <summary>
        /// Render the ID as a string
        /// </summary>
        /// <returns>String value of the ID</returns>
        public override string ToString() => Value.ToString();
    }
}
