namespace Masny.Microservices.Profile.Api.Data.Models
{
    /// <summary>
    /// Profile.
    /// </summary>
    public class Profile
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
