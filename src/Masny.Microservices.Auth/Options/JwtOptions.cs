using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Masny.Microservices.Auth.Options
{
    /// <summary>
    /// Jwt options.
    /// </summary>
    public class JwtOptions
    {
        const string SecretKey = "mysupersecret_secretkey!123";

        /// <summary>
        /// Issuer.
        /// </summary>
        public const string Issuer = "Masny.Microservices.Identity.Api";

        /// <summary>
        /// Audience.
        /// </summary>
        public const string Audience = "Masny.Microservices";

        /// <summary>
        /// Lifetime.
        /// </summary>
        public const int Lifetime = 24;

        /// <summary>
        /// Get symmetric security key.
        /// </summary>
        /// <returns>Key.</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}
