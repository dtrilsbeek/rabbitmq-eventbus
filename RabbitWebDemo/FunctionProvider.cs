using RabbitMQ.Client.Events;
using RabbitMQ_Eventbus.FunctionProvider;
using RabbitMQ_Eventbus.Message;
using System;
using System.Collections.Generic;

namespace RabbitWebDemo
{
    internal class FunctionProvider : RabbitMQFunctionProviderBase
    {
        protected override Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> InializeFunctionsWithKeys()
        {
            return new Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>>();
        }
    }
}