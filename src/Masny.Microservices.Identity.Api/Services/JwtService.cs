using Masny.Microservices.Identity.Api.Interfaces;
using Masny.Microservices.Identity.Api.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Masny.Microservices.Identity.Api.Services
{
    /// <inheritdoc cref="IJwtService"/>
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;

        public JwtService(IOptions<AppSettings> appSettings)
        {
            // UNDONE: check appsettings
            _appSettings = appSettings.Value;
        }

        public string GetAccessToken(IEnumerable<Claim> claims)
        {
            const string issuer = "Masny.Microservices.Identity.Api";
            const string audience = "Masny.Microservices";

            var now = DateTime.UtcNow;

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromHours(_appSettings.Lifetime)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public ClaimsIdentity GetIdentity(string email, string password)
        {
            //Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);

            Person person = new Person
            {
                Login = "test",
                Role = "test"
            };


            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return new ClaimsIdentity();
        }

        private class Person
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}
