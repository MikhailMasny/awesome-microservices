namespace Masny.Microservices.Identity.Api.Contracts.Responses
{
    /// <summary>
    /// Authentication response.
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// Access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Account identifier.
        /// </summary>
        public string AccountId { get; set; }
    }
}
