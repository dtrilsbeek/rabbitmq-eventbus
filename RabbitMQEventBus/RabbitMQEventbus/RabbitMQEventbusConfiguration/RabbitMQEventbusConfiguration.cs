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
        /// </summary>
        public string Hostname { get; set; }

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
