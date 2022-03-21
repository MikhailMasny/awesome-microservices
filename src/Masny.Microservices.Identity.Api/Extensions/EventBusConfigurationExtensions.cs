using Masny.Microservices.EventBus.Settings;
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
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            var eventBusSettingsSection = configuration.GetSection("EventBusSettings");
            var eventBusSettings = eventBusSettingsSection.Get<EventBusSettings>();

            services.AddMassTransit(x =>
            {
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
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
