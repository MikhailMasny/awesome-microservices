namespace Masny.Microservices.Identity.Api.Contracts.Requests
{
    /// <summary>
    /// Authentication request.
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
