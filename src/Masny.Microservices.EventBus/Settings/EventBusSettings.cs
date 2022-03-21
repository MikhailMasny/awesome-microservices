namespace Masny.Microservices.EventBus.Settings
{
    /// <summary>
    /// RabbitMQ event bus settings.
    /// </summary>
    public class EventBusSettings
    {
        /// <summary>
        /// RabbitMQ host name.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port.
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// Event bus (RabbitMQ-based) virtual host name.
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// RabbitMQ user name.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// RabbitMQ password
        /// </summary>
        public string Password { get; set; }
    }
}
