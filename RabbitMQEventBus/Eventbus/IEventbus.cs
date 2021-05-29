namespace RabbitMQEventbus.Eventbus
{
    /// <summary>
    /// Allows publishing and subscribing to a message broker.
    /// </summary>
    /// <typeparam name="T">The message type</typeparam>
    /// <typeparam name="U">The definition of the destination of the message</typeparam>
    public interface IEventbus<T, U> where T : IMessage<U>
    {
        /// <summary>
        /// Gives the status of the connection with the message broker.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Publishes a message to the message broker.
        /// </summary>
        /// <param name="message">The message that is sent to the broker.</param>
        void Publish(T message);
    }
}
