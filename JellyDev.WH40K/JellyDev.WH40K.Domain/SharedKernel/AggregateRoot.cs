using System.Collections.Generic;
using System.Linq;

namespace JellyDev.WH40K.Domain.SharedKernel
{
    /// <summary>
    /// Abstract base class for aggregate roots
    /// </summary>
    /// <typeparam name="TId">ID type of the aggregate root</typeparam>
    public abstract class AggregateRoot<TId> : IDomainEventHandler
    {
        /// <summary>
        /// Aggregate root ID
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Aggregate events
        /// </summary>
        private readonly List<object> _changes;

        /// <summary>
        /// Create a new aggregate root
        /// </summary>
        protected AggregateRoot() => _changes = new List<object>();

        /// <summary>
        /// Accessor for the aggregate events
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        /// <summary>
        /// Clear the aggregate events
        /// </summary>
        public void ClearChanges() => _changes.Clear();

        /// <summary>
        /// Ensure the aggregate is in a valid state
        /// </summary>
        protected abstract void EnsureValidState();

        /// <summary>
        /// Template method for applying an event to the aggregate
        /// </summary>
        /// <param name="event">The event to apply</param>
        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        /// <summary>
        /// Apply an event an entity internal to the aggregate
        /// </summary>
        /// <param name="entity">The internal entity</param>
        /// <param name="event">The event to apply</param>
        protected void ApplyToEntity(IDomainEventHandler entity, object @event)
            => entity?.Handle(@event);

        /// <summary>
        /// Handle an event internal to the aggregate
        /// </summary>
        /// <param name="event">The event to handle</param>
        void IDomainEventHandler.Handle(object @event) => When(@event);

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="event">The event to handle</param>
        protected abstract void When(object @event);
    }
}
