using Masny.Microservices.Common.Settings;
using MassTransit;

namespace Masny.Microservices.Identity.Api.Extensions
{
    /// <summary>
    /// Define extensions to configure event bus.
    /// </summary>
    public static class EventBusConfigurationExtensions
    {
        /// <summary>
        /// Add event bus service (RabbitMQ-based).
        /// </summary>
        /// <param name="services">DI Container.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>Configured event bus service.</returns>
        public static IServiceCollection AddEventBusService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var eventBusSettingsSection = configuration.GetSection("EventBusSettings");
            var eventBusSettings = eventBusSettingsSection.Get<EventBusSettings>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
