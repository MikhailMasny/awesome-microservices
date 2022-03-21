namespace Masny.Microservices.EventBus.Models
{
    /// <summary>
    /// Define message interface for account creation.
    /// </summary>
    public class AccountCreated : EventBase
    {
        /// <summary>
        /// Account Identifier.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName { get; set; }
    }
}
