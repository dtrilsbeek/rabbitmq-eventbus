using System.Collections.Generic;

namespace RabbitMQ_Eventbus.Configuration
{
    /// <summary>
    /// Holds information to declare an exchange for RabbitMQ.
    /// </summary>
    public class Exchange
    {
        /// <summary>
        /// The name of the Exchange.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of exchange that will be declared.
        /// "topic" is used by default.
        /// </summary>
        public string Type { get; set; } = "topic";

        /// <summary>
        /// Determines if the echange is persitent after broker restarts.
        /// <see langword="false"/> by default.
        /// </summary>
        public bool Durable { get; set; } = false;

        /// <summary>
        /// Determines if the exchange will be deleted when the last queue
        /// is unbound from it.
        /// <see langword="true"/> by default.
        /// </summary>
        public bool AutoDelete { get; set; } = true;

        /// <summary>
        /// Additional arguments to configure an exchange.
        /// </summary>
        public Dictionary<string, object> Arguments { get; set; }
    }
}
