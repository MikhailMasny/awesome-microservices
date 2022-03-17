using System.Security.Claims;

namespace Masny.Microservices.Auth.Interfaces
{
    /// <summary>
    /// Jwt service.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Get user claims identity.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <param name="roles">Roles.</param>
        /// <returns>Claims identity</returns>
        public ClaimsIdentity GetIdentity(string accountId, IEnumerable<string> roles);

        /// <summary>
        /// Get access token.
        /// </summary>
        /// <param name="claims">Collection of claims.</param>
        /// <returns>Jwt token.</returns>
        public string GetAccessToken(IEnumerable<Claim> claims);
    }
}
