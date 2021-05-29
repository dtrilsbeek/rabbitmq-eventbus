using System;
using System.Collections.Generic;
using RabbitMQ.Client.Events;
using RabbitMQEventbus.Extension.Message;

namespace RabbitMQEventbus.Extension.FunctionProvider
{
    /// <summary>
    /// The base class of a functionprovider that provides functions that handle incoming RabbitMQ messages.
    /// </summary>
    public abstract class RabbitMQFunctionProviderBase : IRabbitMQFunctionProvider
    {
        private Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> functionsWithKey = new Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>>();

        /// <summary>
        /// Gets the function bound to a key.
        /// </summary>
        /// <param name="key">
        /// The key of the function you want to get.
        /// </param>
        /// <returns>
        /// The function that is bound to the key.
        /// </returns>
        public Func<object, BasicDeliverEventArgs, RabbitMQMessage> GetFunction(string key)
        {
            return functionsWithKey.GetValueOrDefault(key, null);
        }

        /// <summary>
        /// Checks if the key exists.
        /// </summary>
        /// <param name="key">
        /// The key to check.
        /// </param>
        /// <returns>
        /// True if the key exists.
        /// False if the key does not exist.
        /// </returns>
        public bool HasFunction(string key)
        {
            return functionsWithKey.ContainsKey(key);
        }

        public void Subscribe(string key, Func<object, BasicDeliverEventArgs, RabbitMQMessage> function)
        {
            if (functionsWithKey.ContainsKey(key))
            {
                return;
            }

            functionsWithKey.Add(key, function);
        }

        public RabbitMQFunctionProviderBase()
        {
            functionsWithKey = InializeFunctionsWithKeys();
        }

        /// <summary>
        /// Gives all functions bound by a key at the start of the RabbitMQEventbus.
        /// </summary>
        /// <returns>
        /// All functions bound by their keys.
        /// </returns>
        protected abstract Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> InializeFunctionsWithKeys();
    }
}
