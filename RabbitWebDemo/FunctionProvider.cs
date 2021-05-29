using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQEventbus.Extension.FunctionProvider;
using RabbitMQEventbus.Extension.Message;

namespace RabbitWebDemo
{
	internal class FunctionProvider : RabbitMQFunctionProviderBase
	{
		protected override Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>>
			InializeFunctionsWithKeys()
		{
			var dictionary = new Dictionary<string, Func<object, BasicDeliverEventArgs, RabbitMQMessage>>();
			dictionary.Add(nameof(HandleFillPartialTweet), HandleFillPartialTweet);
			dictionary.Add(nameof(HandleCreatedTweet), HandleCreatedTweet);
			
			return dictionary;
		}

		private RabbitMQMessage HandleFillPartialTweet(object o, BasicDeliverEventArgs eventArgs)
		{
			var body = eventArgs.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			var extended = message + " userdata";

			Console.WriteLine("HandleFillPartialTweet");
			Console.WriteLine(extended);
			
			return new RabbitMQMessage(null, extended);
		}
		
		private RabbitMQMessage HandleCreatedTweet(object o, BasicDeliverEventArgs eventArgs)
		{
			var body = eventArgs.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);

			Console.WriteLine("HandleCreatedTweet!");
			Console.WriteLine(message);

			return null;
		}
	}
}