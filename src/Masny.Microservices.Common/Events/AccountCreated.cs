namespace Masny.Microservices.Common.Events
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
    }
}
