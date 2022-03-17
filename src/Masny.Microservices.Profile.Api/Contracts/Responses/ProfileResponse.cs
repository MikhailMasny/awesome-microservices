namespace Masny.Microservices.Profile.Api.Contracts.Responses
{
    /// <summary>
    /// Profile data response.
    /// </summary>
    public class ProfileResponse
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Account identifier.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName { get; set; }
    }
}
