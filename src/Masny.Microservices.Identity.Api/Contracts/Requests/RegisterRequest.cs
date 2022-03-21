namespace Masny.Microservices.Identity.Api.Contracts.Requests
{
    /// <summary>
    /// Register request.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password confirm.
        /// </summary>
        public string PasswordConfirm { get; set; }
    }
}
