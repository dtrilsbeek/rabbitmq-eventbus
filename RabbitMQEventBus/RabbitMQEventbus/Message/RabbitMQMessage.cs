using RabbitMQ_Eventbus.Eventbus;

namespace RabbitMQ_Eventbus.Message
{
    /// <summary>
    /// A message that will be sent to the RabbitMQ broker.
    /// </summary>
    public class RabbitMQMessage : IMessage<MessageDestination>
    {
        /// <summary>
        /// The destination of the message.
        /// </summary>
        public MessageDestination Destination { get; }

        /// <summary>
        /// The message payload.
        /// </summary>
        public string Message { get; }

        public RabbitMQMessage(MessageDestination destination, string message)
        {
            Destination = destination;
            Message = message;
        }
    }
}
