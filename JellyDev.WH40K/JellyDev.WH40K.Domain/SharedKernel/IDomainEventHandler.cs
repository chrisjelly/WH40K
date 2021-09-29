

namespace JellyDev.WH40K.Domain.SharedKernel
{
    /// <summary>
    /// Domain event handler interface
    /// </summary>
    public interface IDomainEventHandler
    {
        /// <summary>
        /// Handle a domain event
        /// </summary>
        /// <param name="event">The event to handle</param>
        void Handle(object @event);
    }
}
