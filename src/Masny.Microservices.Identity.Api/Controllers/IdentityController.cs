using Masny.Microservices.Identity.Api.Contracts.Requests;
using Masny.Microservices.Identity.Api.Contracts.Responses;
using Masny.Microservices.Identity.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Masny.Microservices.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public IdentityController(IJwtService jwtService)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        [HttpPost("/get-token")]
        public IActionResult Token([FromBody] AuthenticationRequest request)
        {
            var identity = _jwtService.GetIdentity(request.Email, request.Password);
            if (!identity.Claims.Any())
            {
                return BadRequest(new
                {
                    error = "Invalid username or password."
                });
            }

            var response = new AuthenticationResponse
            {
                AccessToken = _jwtService.GetAccessToken(new List<Claim>()),
                Email = identity.Name ?? string.Empty,
            };

            return Ok(response);
        }
    }
}
