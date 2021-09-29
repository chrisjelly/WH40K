using JellyDev.WH40K.Domain.SharedKernel;
using System;
using static JellyDev.WH40K.Domain.SharedKernel.Exceptions;

namespace JellyDev.WH40K.Domain.Stratagem
{
    /// <summary>
    /// Stratagem aggregate root
    /// </summary>
    public class StratagemAggregateRoot : AggregateRoot<StratagemId>
    {
        /// <summary>
        /// Protected constructor
        /// </summary>
        protected StratagemAggregateRoot() { }

        /// <summary>
        /// The phases relevant to this stratagem
        /// </summary>
        public Phase[] Phases { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID</param>
        public StratagemAggregateRoot(StratagemId id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        /// <summary>
        /// Ensure the aggregate is in a valid state
        /// </summary>
        protected override void EnsureValidState()
        {
            bool valid = true;

            if (!valid) throw new InvalidEntityStateException(this, $"Post-checks failed for stratagem");
        }

        /// <summary>
        /// Create a new stratagem
        /// </summary>
        /// <param name="phases">The phases relevant to this stratagem</param>
        public void Create(Phase[] phases)
        {
            if (phases == null) throw new ArgumentNullException(nameof(phases));

            Apply(new Events.StratagemCreated
            {
                Id = Id,
                Phases = phases
            });
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
            }
        }

        /// <summary>
        /// Stratagem Created event handler
        /// </summary>
        /// <param name="e">Stratagem Created event</param>
        private void HandleStratagemCreated(Events.StratagemCreated e)
        {
            // TODO: Implement!

            throw new NotImplementedException();
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
