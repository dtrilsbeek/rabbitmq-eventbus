using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQEventbus.Extension.FunctionProvider;
using RabbitMQEventbus.Extension.Message;

namespace RabbitDemo
{
    internal class FunctionProvider : RabbitMQFunctionProviderBase
    {
        protected override Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> InializeFunctionsWithKeys()
        {
            Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>> dictionary = new();
            dictionary.Add("number", PrintNumber);
            dictionary.Add(nameof(HandleCreateFilledTweet), HandleCreateFilledTweet);
            return dictionary;
        }

        private RabbitMQMessage PrintNumber(object o, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            return null;
        }

        private RabbitMQMessage HandleCreateFilledTweet(object o, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine("HandleCreateFilledTweet");
            Console.WriteLine(message);

            return new RabbitMQMessage(null, "tweet is created: tweetinfo");
        }
    }
}
