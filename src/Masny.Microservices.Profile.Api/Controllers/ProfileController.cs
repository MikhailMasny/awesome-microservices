using Masny.Microservices.Profile.Api.Contracts.Requests;
using Masny.Microservices.Profile.Api.Contracts.Responses;
using Masny.Microservices.Profile.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Masny.Microservices.Profile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileManager _profileManager;

        public ProfileController(IProfileManager profileManager)
        {
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(string accountId)
        {
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
