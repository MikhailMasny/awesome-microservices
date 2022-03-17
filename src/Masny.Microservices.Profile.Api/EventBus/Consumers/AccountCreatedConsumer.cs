using Masny.Microservices.Common.Events;
using Masny.Microservices.Profile.Api.Interfaces;
using MassTransit;

namespace Masny.Microservices.Profile.Api.EventBus.Consumers
{
    /// <summary>
    ///  Define consumer of "account created" event.
    /// </summary>
    public class AccountCreatedConsumer : IConsumer<AccountCreated>
    {
        private readonly IProfileManager _profileManager;

        /// <summary>
        /// Constructor of the consumer of "account created" events.
        /// </summary>
        /// <param name="profileManager">Profile manager.</param>
        public AccountCreatedConsumer(IProfileManager profileManager)
        {
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        }

        /// <summary>
        /// Consume "account created" event.
        /// </summary>
        /// <param name="context">Event context.</param>
        public async Task Consume(ConsumeContext<AccountCreated> context)
        {
            try
            {
                var accountId = context.Message.AccountId;
                await _profileManager.CreateAsync(accountId);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"{AccountConstants.EVENT_BUS_CONSUMER_ERROR}: {ex.Message}");
            }
        }
    }
}
