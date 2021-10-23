using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.Keyword.ParameterObjects;
using JellyDev.WH40K.Domain.SharedKernel;
using JellyDev.WH40K.Domain.SharedKernel.Interfaces;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using System;
using static JellyDev.WH40K.Domain.SharedKernel.Exceptions;

namespace JellyDev.WH40K.Domain.Keyword
{
    /// <summary>
    /// Keyword aggregate root
    /// </summary>
    public class KeywordAggregate : AggregateRoot<KeywordId>
    {
        /// <summary>
        /// Protected constructor
        /// </summary>
        protected KeywordAggregate() { }

        /// <summary>
        /// ID of the faction owning this stratagem
        /// </summary>
        public FactionId FactionId { get; private set; }

        /// <summary>
        /// Name of the keyword
        /// </summary>
        public Name Name { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createKeywordParams">Parameter object for creating a keyword</param>
        /// <param name="factionChecker">Faction checker for confirming factions exist</param>
        public KeywordAggregate(CreateKeywordParams createKeywordParams, IRepositoryChecker<FactionId> factionChecker)
        {
            if (createKeywordParams == null) throw new ArgumentNullException(nameof(createKeywordParams));
            if (factionChecker == null) throw new ArgumentNullException(nameof(factionChecker));

            if (createKeywordParams.FactionId != Guid.Empty && factionChecker.Exists(createKeywordParams.FactionId) == false)
                throw new Exception($"Unable to find faction with id {createKeywordParams.FactionId}");

            Apply(new Events.KeywordCreated
            {
                Id = createKeywordParams.Id,
                FactionId = createKeywordParams.FactionId,
                Name = createKeywordParams.Name
            });
        }

        /// <summary>
        /// Update the keyword
        /// </summary>
        /// <param name="updateKeywordParams">Parameter object for updating a keyword</param>
        /// <param name="factionChecker">Faction checker for confirming factions exist</param>
        public void Update(UpdateKeywordParams updateKeywordParams, IRepositoryChecker<FactionId> factionChecker)
        {
            if (updateKeywordParams == null) throw new ArgumentNullException(nameof(updateKeywordParams));
            if (factionChecker == null) throw new ArgumentNullException(nameof(factionChecker));

            if (updateKeywordParams.FactionId != Guid.Empty && factionChecker.Exists(updateKeywordParams.FactionId) == false)
                throw new Exception($"Unable to find faction with id {updateKeywordParams.FactionId}");

            Apply(new Events.KeywordUpdated
            {
                Id = Id,
                FactionId = updateKeywordParams.FactionId,
                Name = updateKeywordParams.Name
            });
        }

        /// <summary>
        /// Delete the keyword
        /// </summary>
        public void Delete()
        {
            Apply(new Events.KeywordDeleted
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
                FactionId != null &&
                Name != null;

            if (!valid) throw new InvalidEntityStateException(this, $"Post-checks failed for keyword");
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="event">The event to handle</param>
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.KeywordCreated e:
                    HandleKeywordCreated(e);
                    break;
                case Events.KeywordUpdated e:
                    HandleKeywordUpdated(e);
                    break;
            }
        }

        /// <summary>
        /// Keyword Created event handler
        /// </summary>
        /// <param name="e">Keyword Created event</param>
        private void HandleKeywordCreated(Events.KeywordCreated e)
        {
            Id = new KeywordId(e.Id);
            FactionId = new FactionId(e.FactionId);
            Name = new Name(e.Name);
        }

        /// <summary>
        /// Keyword Updated event handler
        /// </summary>
        /// <param name="e">Keyword Updated event</param>
        private void HandleKeywordUpdated(Events.KeywordUpdated e)
        {
            FactionId = new FactionId(e.FactionId);
            Name = new Name(e.Name);
        }
    }

    /// <summary>
    /// Keyword ID
    /// </summary>
    public class KeywordId : Value<KeywordId>
    {
        /// <summary>
        /// Value
        /// </summary>
        public Guid Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected KeywordId() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value to use for the keyword ID</param>
        public KeywordId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Keyword id cannot be empty");
            Value = value;
        }

        /// <summary>
        /// Implicit operator to GUID
        /// </summary>
        /// <param name="self">Self reference</param>
        public static implicit operator Guid(KeywordId self)
        {
            if (self == null) return Guid.Empty;
            else return self.Value;
        }

        /// <summary>
        /// Implicit operator from string
        /// </summary>
        /// <param name="value">String value</param>
        public static implicit operator KeywordId(string value) => new KeywordId(Guid.Parse(value));

        /// <summary>
        /// Render the ID as a string
        /// </summary>
        /// <returns>String value of the ID</returns>
        public override string ToString() => Value.ToString();
    }
}
