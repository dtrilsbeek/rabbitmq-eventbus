using System.Collections.Generic;

namespace RabbitMQ_Eventbus.Configuration
{
    /// <summary>
    /// Holds all data to configure rabbitMQ for this instance.
    /// </summary>
    public class RabbitMQEventbusConfiguration
    {
        /// <summary>
        /// The hostname of the RabbitMQ broker.
        /// The default value is "localhost".
        /// </summary>
        public string Hostname { get; set; } = "localhost";

        /// <summary>
        /// The username that is used to connect to the RabbitMQ broker.
        /// The default value is "guest".
        /// </summary>
        public string Username { get; set; } = "guest";

        /// <summary>
        /// The password that is used to connect to the RabbitMQ broker.
        /// The default value is "guest".
        /// </summary>
        public string Password { get; set; } = "guest";

        /// <summary>
        /// The port that is used to connect to the RabbitMQ broker.
        /// The default value is 5672.
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// The exchanges that will be declared.
        /// </summary>
        public List<Exchange> Exchanges = new();

        /// <summary>
        /// The queues that will be declared.
        /// </summary>
        public List<Queue> Queues = new();

        /// <summary>
        /// The subscriptions tht will be configured.
        /// </summary>
        public List<Subscription> Upstream = new();
    }
}
