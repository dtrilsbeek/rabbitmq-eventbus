using System;

namespace RabbitMQEventbus.Eventbus
{
    /// <summary>
    /// Provides functions that handle incoming messages from the message broker.
    /// </summary>
    /// <typeparam name="S">Type of the delivery arguments</typeparam>
    /// <typeparam name="T">The message type</typeparam>
    /// <typeparam name="U">The definition of the destination of the message</typeparam>
    public interface IFunctionProvider<S, T, U> where T : IMessage<U>
    {
        /// <summary>
        /// Checks if a function with a certain key exists.
        /// </summary>
        /// <param name="key">The key of the function that is checked.</param>
        /// <returns></returns>
        bool HasFunction(string key);

        /// <summary>
        /// Gets a function with a specified key.
        /// </summary>
        /// <param name="key">The key of the function to get.</param>
        /// <returns>The function with the specified key.</returns>
        Func<object, S, T> GetFunction(string key);

        /// <summary>
        /// Subscibes a function with a given key.
        /// </summary>
        /// <param name="key">The key with which the function is subscribed.</param>
        /// <param name="function">The function that is subscribed.</param>
        void Subscribe(string key, Func<object, S, T> function);
    }
}
