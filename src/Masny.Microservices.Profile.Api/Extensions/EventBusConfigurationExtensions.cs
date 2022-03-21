using Masny.Microservices.EventBus.Settings;
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
            var eventBusSettingsSection = configuration.GetSection("EventBusSettings");
            var eventBusSettings = eventBusSettingsSection.Get<EventBusSettings>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AccountCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    if (environment.IsProduction())
                    {
                        cfg.ConfigureEndpoints(context);

                        cfg.Host(
                            eventBusSettings.Host,
                            eventBusSettings.Port,
                            eventBusSettings.VirtualHost,
                            h =>
                            {
                                h.Username(eventBusSettings.User);
                                h.Password(eventBusSettings.Password);
                            });
                    }

                    cfg.ReceiveEndpoint(EventBusQueueNames.AccountEvents, e =>
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
