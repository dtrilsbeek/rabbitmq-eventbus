using RabbitMQ.Client.Events;
using RabbitMQEventbus.Eventbus;
using RabbitMQEventbus.Extension.Message;

namespace RabbitMQEventbus.Extension.FunctionProvider
{
    /// <summary>
    /// Provides functions that handle incoming messages from the RabbitMQ broker.
    /// </summary>
    public interface IRabbitMQFunctionProvider : IFunctionProvider<BasicDeliverEventArgs, RabbitMQMessage, MessageDestination>
    {

    }
}
