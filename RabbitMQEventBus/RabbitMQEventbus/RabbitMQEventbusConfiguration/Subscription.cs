namespace RabbitMQ_Eventbus.Configuration
{
    /// <summary>
    /// Describes an endpoint where messages coming from RabbitMQ
    /// will be handled.
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// The key which determines what function will be called when
        /// an incoming message needs to be handled.
        /// </summary>
        public string FunctionKey { get; set; }

        /// <summary>
        /// The exchange that will be used to bind the queue.
        /// If the exchange is not seperately declared, a default
        /// exchange will be declared using this name.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// The queuename that will be given to the queue.
        /// If left empty, an anounymous queue will be generated.
        /// </summary>
        public string QueueName { get; set; } = "";

        /// <summary>
        /// The route that will be used to bind the queue.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Determines whether a message will be acknowledged automaticly.
        /// <see langword="true"/> by default.
        /// </summary>
        public bool AutoAck { get; set; } = true;

        /// <summary>
        /// The destination of a response message.
        /// No response message will be sent if <see langword="null"/>.
        /// </summary>
        public Publication Downstream { get; set; }
    }
}
