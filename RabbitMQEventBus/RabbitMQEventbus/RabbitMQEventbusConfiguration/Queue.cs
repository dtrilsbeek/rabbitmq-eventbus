using System.Collections.Generic;

namespace RabbitMQ_Eventbus.Configuration
{
    /// <summary>
    /// Holds information to configure a queue for RabbitMQ.
    /// </summary>
    public class Queue
    {
        /// <summary>
        /// The name of the Queue.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if the queue is persitent after broker restarts.
        /// <see langword="false"/> by default.
        /// </summary>
        public bool Durable { get; set; } = false;

        /// <summary>
        /// Determines if the queue can only be used by the instance
        /// that created the queue.
        /// The Exclusive queue lifecycle is determined by the connection
        /// and does not survive a node restart, even if <see cref="Durable"/> is <see langword="true"/>.
        /// <see langword="false"/> by default.
        /// </summary>
        public bool Exclusive { get; set; } = false;

        /// <summary>
        /// Additional arguments to configure a queue.
        /// </summary>
        public Dictionary<string, object> Arguments { get; set; }

        /// <summary>
        /// Used during configuration to determine if a queue has already been bound.
        /// </summary>
        public bool Bound { get; set; }
    }
}
