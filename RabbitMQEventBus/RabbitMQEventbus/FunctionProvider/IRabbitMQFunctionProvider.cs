using RabbitMQ.Client.Events;
using RabbitMQ_Eventbus.Eventbus;
using RabbitMQ_Eventbus.Message;

namespace RabbitMQ_Eventbus.FunctionProvider
{
    /// <summary>
    /// Provides functions that handle incoming messages from the RabbitMQ broker.
    /// </summary>
    public interface IRabbitMQFunctionProvider : IFunctionProvider<BasicDeliverEventArgs, RabbitMQMessage, MessageDestination>
    {

    }
}
