using Masny.Microservices.Auth.Extensions;
using Masny.Microservices.Profile.Api.Contracts.Requests;
using Masny.Microservices.Profile.Api.Contracts.Responses;
using Masny.Microservices.Profile.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masny.Microservices.Profile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileManager _profileManager;
        private readonly ILogger _logger;

        public ProfileController(
            IProfileManager profileManager,
            ILogger<ProfileController> logger)
        {
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpGet(ApiRoute.PlatformRoute.Get, Name = nameof(GetPlatform))]

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(string accountId)
        {
            var accountIdByAccessToken = HttpContext.GetUserId();

            if (string.IsNullOrWhiteSpace(accountId) || accountIdByAccessToken != accountId)
            {
                return BadRequest();
            }

            var result = await _profileManager.ReadAsync(accountId);
            if (result.Id == 0)
            {
                return NotFound();
            }

            var profileDataResponse = new ProfileResponse
            {
                Id = result.Id,
                AccountId = result.AccountId,
                FullName = result.FullName,
            };

            return Ok(profileDataResponse);
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> Put(string accountId, [FromBody] UpdateProfileRequest request)
        {
            var result = await _profileManager.UpdateAsync(accountId, request.FullName);

            return result 
                ? Ok()
                : BadRequest();
        }
    }
}
