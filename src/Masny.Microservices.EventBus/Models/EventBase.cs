namespace Masny.Microservices.EventBus.Models
{
    /// <summary>
    /// Define class for message broker events.
    /// </summary>
    public class EventBase
    {
        /// <summary>
        /// Event Identifier.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Event creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
