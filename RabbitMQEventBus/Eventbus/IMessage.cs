namespace RabbitMQ_Eventbus.Eventbus
{
    /// <summary>
    /// The message that will be sent to the message broker.
    /// </summary>
    /// <typeparam name="U">The definition of the destination of the message</typeparam>
    public interface IMessage<U>
    {
        /// <summary>
        /// Gets the destination of the message.
        /// </summary>
        U Destination { get; }
    }
}
