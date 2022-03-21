namespace Masny.Microservices.Profile.Api.Interfaces
{
    /// <summary>
    /// Interface for profile manager.
    /// </summary>
    public interface IProfileManager
    {
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <param name="fullName">Full name.</param>
        Task CreateAsync(string accountId, string fullName);

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Profile data transfer object.</returns>
        Task<Models.ProfileDto> ReadAsync(string accountId);

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <param name="fullName">Full name.</param>
        /// <returns>Operation result.</returns>
        Task<bool> UpdateAsync(string accountId, string fullName);

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        Task DeleteAsync(string accountId);
    }
}
