namespace Masny.Microservices.Identity.Api.Settings
{
    /// <summary>
    /// Application settings from json.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Secret key for Jwt token.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Lifetime Jwt token.
        /// </summary>
        public double Lifetime { get; set; }
    }
}
