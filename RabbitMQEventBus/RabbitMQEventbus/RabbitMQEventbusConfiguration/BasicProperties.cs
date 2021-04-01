namespace RabbitMQ_Eventbus.Configuration
{
    /// <summary>
    /// Holds properties to configure a <see cref="Publication"/> message
    /// And reflects the AMQP BasicProperties content header. 
    /// </summary>
    public class BasicProperties
    {
        /// <summary>
        /// Determines whether a message has to be saved to disc
        /// by RabbitMQ.
        /// </summary>
        public bool Persistent { get; set; }
    }
}
