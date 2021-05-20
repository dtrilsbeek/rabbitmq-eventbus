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
            Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> dictionary = new();
            dictionary.Add("number", PrintNumber);
            dictionary.Add("HandleWindData", HandleWindData);
            dictionary.Add("HandleSolarData", HandleSolarData);
            return dictionary;
        }

        private RabbitMQMessage PrintNumber(object o, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            return null;
        }
        
        
        private RabbitMQMessage HandleSolarData(object o, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            return null;
        }

        private RabbitMQMessage HandleWindData(object o, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            return null;
        }
    }
}
