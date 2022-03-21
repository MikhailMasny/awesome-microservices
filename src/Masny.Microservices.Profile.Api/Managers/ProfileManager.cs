using Masny.Microservices.Profile.Api.Data;
using Masny.Microservices.Profile.Api.Interfaces;
using Masny.Microservices.Profile.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Masny.Microservices.Profile.Api.Managers
{
    /// <inheritdoc cref="IProfileManager"/>
    public class ProfileManager : IProfileManager
    {
        private readonly ApplicationContext _applicationContext;

        public ProfileManager(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task CreateAsync(string accountId, string fullName)
        {
            var profile = new Data.Models.Profile
            {
                AccountId = accountId.ToString(),
                FullName = fullName,
            };

            await _applicationContext.Profiles.AddAsync(profile);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<ProfileDto> ReadAsync(string accountId)
        {
            var profile = await _applicationContext.Profiles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.AccountId == accountId);

            return profile is null
                ? new ProfileDto()
                : new ProfileDto
                {
                    Id = profile.Id,
                    AccountId = profile.AccountId,
                    FullName = profile.FullName,
                };
        }

        public async Task<bool> UpdateAsync(string accountId, string fullName)
        {
            var profile = await _applicationContext.Profiles.SingleOrDefaultAsync(x => x.AccountId == accountId);
            if (profile is null && profile?.FullName == fullName)
            {
                return false;
            }

            profile.FullName = fullName;
            await _applicationContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(string accountId)
        {
            var profile = await _applicationContext.Profiles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.AccountId == accountId);

            if (profile is not null)
            {
                _applicationContext.Profiles.Remove(profile);
                await _applicationContext.SaveChangesAsync();
            }
        }
    }
}
