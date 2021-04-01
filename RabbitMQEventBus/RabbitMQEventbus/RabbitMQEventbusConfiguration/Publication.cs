namespace RabbitMQ_Eventbus.Configuration
{
    /// <summary>
    /// Describes the destination of a response message when a message
    /// is recieved.
    /// </summary>
    public class Publication
    {
        /// <summary>
        /// The name of the exchange where the message will be published.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// The route that will be used to publish the message.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Properties to configure a publication message.
        /// </summary>
        public BasicProperties BasicProperties { get; set; }
    }
}
