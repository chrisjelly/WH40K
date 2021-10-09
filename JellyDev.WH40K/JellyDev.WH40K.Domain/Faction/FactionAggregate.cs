using JellyDev.WH40K.Domain.Faction.ParameterObjects;
using JellyDev.WH40K.Domain.SharedKernel;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;
using static JellyDev.WH40K.Domain.SharedKernel.Exceptions;

namespace JellyDev.WH40K.Domain.Faction
{
    /// <summary>
    /// Faction aggregate root
    /// </summary>
    public class FactionAggregate: AggregateRoot<FactionId>
    {
        /// <summary>
        /// Protected constructor
        /// </summary>
        protected FactionAggregate() { }

        /// <summary>
        /// Name of the faction
        /// </summary>
        public Name Name { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createFactionParams">Parameter object for creating a faction</param>
        public FactionAggregate(CreateFactionParams createFactionParams)
        {
            if (createFactionParams == null) throw new ArgumentNullException(nameof(createFactionParams));

            Apply(new Events.FactionCreated
            {
                Id = createFactionParams.Id,
                Name = createFactionParams.Name
            });
        }

        /// <summary>
        /// Update the faction
        /// </summary>
        /// <param name="updateFactionParams">Parameter object for updating a faction</param>
        public void Update(UpdateFactionParams updateFactionParams)
        {
            if (updateFactionParams == null) throw new ArgumentNullException(nameof(updateFactionParams));

            Apply(new Events.FactionUpdated
            {
                Id = Id,
                Name = updateFactionParams.Name
            });
        }

        /// <summary>
        /// Delete the faction
        /// </summary>
        public void Delete()
        {
            Apply(new Events.FactionDeleted
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
                string.IsNullOrEmpty(Name) == false;

            if (!valid) throw new InvalidEntityStateException(this, $"Post-checks failed for faction");
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="event">The event to handle</param>
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.FactionCreated e:
                    HandleFactionCreated(e);
                    break;
                case Events.FactionUpdated e:
                    HandleFactionUpdated(e);
                    break;
            }
        }

        /// <summary>
        /// Faction Created event handler
        /// </summary>
        /// <param name="e">Faction Created event</param>
        private void HandleFactionCreated(Events.FactionCreated e)
        {
            Id = new FactionId(e.Id);
            Name = Name.FromString(e.Name);
        }

        /// <summary>
        /// Faction Updated event handler
        /// </summary>
        /// <param name="e">Faction Updated event</param>
        private void HandleFactionUpdated(Events.FactionUpdated e)
        {
            Name = Name.FromString(e.Name);
        }
    }

    /// <summary>
    /// Faction ID
    /// </summary>
    public class FactionId : Value<FactionId>
    {
        /// <summary>
        /// Value
        /// </summary>
        public Guid Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected FactionId() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value to use for the faction ID</param>
        public FactionId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Faction id cannot be empty");
            Value = value;
        }

        /// <summary>
        /// Implicit operator to GUID
        /// </summary>
        /// <param name="self">Self reference</param>
        public static implicit operator Guid(FactionId self)
        {
            if (self == null) return Guid.Empty;
            else return self.Value;
        }

        /// <summary>
        /// Implicit operator from string
        /// </summary>
        /// <param name="value">String value</param>
        public static implicit operator FactionId(string value) => new FactionId(Guid.Parse(value));

        /// <summary>
        /// Render the ID as a string
        /// </summary>
        /// <returns>String value of the ID</returns>
        public override string ToString() => Value.ToString();
    }
}
