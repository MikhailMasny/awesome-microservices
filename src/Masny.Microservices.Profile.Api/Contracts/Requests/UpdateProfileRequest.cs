namespace Masny.Microservices.Profile.Api.Contracts.Requests
{
    /// <summary>
    /// Update profile request.
    /// </summary>
    public class UpdateProfileRequest
    {
        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName { get; set; }
    }
}
