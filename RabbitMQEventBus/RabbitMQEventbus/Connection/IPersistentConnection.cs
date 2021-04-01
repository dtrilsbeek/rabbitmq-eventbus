using RabbitMQ.Client;

namespace RabbitMQ_Eventbus.Connection
{
    /// <summary>
    /// A persistent connection to the RabbitMQ broker.
    /// </summary>
    public interface IPersistentConnection
    {
        bool IsConnected { get; }

        /// <summary>
        /// Try to establish a connection with the RabbitMQ broker.
        /// </summary>
        bool TryConnect();

        /// <summary>
        /// Create a model to interact with the RabbbitMQ broker.
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}
