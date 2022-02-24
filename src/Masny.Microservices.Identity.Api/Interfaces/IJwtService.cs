using System.Security.Claims;

namespace Masny.Microservices.Identity.Api.Interfaces
{
    /// <summary>
    /// Jwt service.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Get access token.
        /// </summary>
        /// <param name="claims">Collection of claims.</param>
        /// <returns>Jwt token.</returns>
        string GetAccessToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Get user claims identity.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Claims identity</returns>
        ClaimsIdentity GetIdentity(string email, string password);
    }
}
