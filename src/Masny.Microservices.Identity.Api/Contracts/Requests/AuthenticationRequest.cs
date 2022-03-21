namespace Masny.Microservices.Identity.Api.Contracts.Requests
{
    /// <summary>
    /// Authentication request.
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
