using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.IO;
using RabbitMQEventbus.Extension.Connection;
using RabbitMQEventbus.Extension.Message;
using RabbitMQEventbus.Extension.RabbitMQEventbusConfiguration;

namespace RabbitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting number printer...");

            string path = Environment.CurrentDirectory + @"/RabbitMQEventbus.json";
            var file = File.ReadAllText(path);

            var config = JsonConvert.DeserializeObject<RabbitMQEventbusConfiguration>(file);

            var connectionLogger = new Logger<PersistentConnection>(new NullLoggerFactory());
            var connectionFactory = new ConnectionFactory() { HostName = config.Hostname };
            var connection = new PersistentConnection(connectionFactory, connectionLogger);


            var eventbusLogger = new Logger<RabbitMQEventbus.Extension.Eventbus.RabbitMQEventbus>(new NullLoggerFactory());

            var functionProvider = new FunctionProvider();

            RabbitMQEventbus.Extension.Eventbus.RabbitMQEventbus eventbus = new RabbitMQEventbus.Extension.Eventbus.RabbitMQEventbus(config, connection,functionProvider,eventbusLogger);

            var message = "Tweet Partial";
            eventbus.Publish(new RabbitMQMessage(new MessageDestination("TweetsExchange", "tweets.create.partial"), message));

            Console.WriteLine("Numbers:");

            Console.ReadLine();
        }
    }
}
