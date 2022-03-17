using Masny.Microservices.Common.Settings;
using Masny.Microservices.Profile.Api.EventBus.Consumers;
using MassTransit;

namespace Masny.Microservices.Profile.Api.Extensions
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
        /// <param name="environment">Hosting environment.</param>
        /// <returns>Configured event bus service.</returns>
        public static IServiceCollection AddEventBusService(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<AccountCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<AccountCreatedConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
