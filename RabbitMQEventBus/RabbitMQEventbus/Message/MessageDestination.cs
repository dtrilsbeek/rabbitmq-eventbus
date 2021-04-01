namespace RabbitMQ_Eventbus.Message
{
    /// <summary>
    /// The destination of a message to a RabbitMQ broker.
    /// </summary>
    public class MessageDestination
    {
        /// <summary>
        /// The exchange the message will be sent to.
        /// </summary>
        public string Exchange { get; }

        /// <summary>
        /// The route the message will be sent to.
        /// </summary>
        public string Route { get; }

        public MessageDestination(string exchange, string route)
        {
            Exchange = exchange;
            Route = route;
        }
    }
}
