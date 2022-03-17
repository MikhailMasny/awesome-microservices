namespace Masny.Microservices.Common.Events
{
    /// <summary>
    /// Define message interface for account deletion.
    /// </summary>
    public class AccountDeleted : EventBase
    {
        /// <summary>
        /// Account Identifier.
        /// </summary>
        public string AccountId { get; set; }
    }
}
