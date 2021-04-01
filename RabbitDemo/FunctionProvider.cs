using RabbitMQ.Client.Events;
using RabbitMQ_Eventbus.FunctionProvider;
using RabbitMQ_Eventbus.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitDemo
{
    internal class FunctionProvider : RabbitMQFunctionProviderBase
    {
        protected override Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> InializeFunctionsWithKeys()
        {
            Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> functions = new();
            functions.Add("number", PrintNumber);
            return functions;
        }
        
        [Functionkey(Key = "number")]
        private RabbitMQMessage PrintNumber(object o, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            return null;
        }
    }
}
