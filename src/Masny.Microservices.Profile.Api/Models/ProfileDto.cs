namespace Masny.Microservices.Profile.Api.Models
{
    /// <summary>
    /// Profile data transfer object.
    /// </summary>
    public class ProfileDto
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
