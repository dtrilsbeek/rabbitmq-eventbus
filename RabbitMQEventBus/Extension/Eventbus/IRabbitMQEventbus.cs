using RabbitMQEventbus.Eventbus;
using RabbitMQEventbus.Extension.Message;

namespace RabbitMQEventbus.Extension.Eventbus
{
    /// <summary>
    /// Allows publishing and subscribing to the RabbitMQ broker.
    /// </summary>
    public interface IRabbitMQEventbus : IEventbus<RabbitMQMessage, MessageDestination>
    {
        /// <summary>
        /// Gives the status of the RabbitMQEventbus.
        /// </summary>
        bool Configured { get; }
    }
}
