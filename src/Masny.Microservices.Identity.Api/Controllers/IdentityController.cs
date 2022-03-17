using Masny.Microservices.Auth.Interfaces;
using Masny.Microservices.Common.Events;
using Masny.Microservices.Identity.Api.Contracts.Requests;
using Masny.Microservices.Identity.Api.Contracts.Responses;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Masny.Microservices.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IPublishEndpoint _publishEndpoint;

        public IdentityController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IJwtService jwtService,
            IPublishEndpoint publishEndpoint)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        #region TEST

        [HttpPost("/test")]
        public async Task<IActionResult> TokenAsync()
        {
            var email = "qwe2";
            var password = "qwe1!Q";

            var signInResult = await _signInManager.PasswordSignInAsync(
                email,
                password,
                false,
                false);

            if (!signInResult.Succeeded)
            {
                return BadRequest(new
                {
                    error = "Invalid username or password."
                });
            }

            var user = await _userManager.FindByNameAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);
            var identity = _jwtService.GetIdentity(user.Id, userRoles);

            var response = new AuthenticationResponse
            {
                AccessToken = _jwtService.GetAccessToken(identity.Claims),
                Email = identity.Name ?? string.Empty,
            };

            return Ok(response);
        }

        #endregion

        [HttpPost("/get-token")]
        public async Task<IActionResult> TokenAsync([FromBody] AuthenticationRequest request)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                request.Email,
                request.Password,
                false,
                false);

            if (!signInResult.Succeeded)
            {
                return BadRequest(new
                {
                    error = "Invalid username or password."
                });
            }

            var user = await _userManager.FindByNameAsync(request.Email);
            var userRoles = await _userManager.GetRolesAsync(user);
            var identity = _jwtService.GetIdentity(user.Id, userRoles);

            var response = new AuthenticationResponse
            {
                AccessToken = _jwtService.GetAccessToken(identity.Claims),
                Email = identity.Name ?? string.Empty,
            };

            return Ok(response);
        }


        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new IdentityUser { Email = request.Email, UserName = request.UserName };
            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            // UNDONE: replace to constants with use default data seed
            await _userManager.AddToRoleAsync(user, "User");

            await _publishEndpoint.Publish(new AccountCreated
            {
                EventId = Guid.NewGuid(),
                AccountId = user.Id,
                CreationDate = DateTime.Now,
            });

            return Ok();
        }
    }
}
