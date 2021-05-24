using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ_Eventbus.Configuration;
using RabbitMQ_Eventbus.Connection;
using RabbitMQ_Eventbus.Eventbus;
using System;
using System.IO;
using RabbitMQ_Eventbus.Message;

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


            var eventbusLogger = new Logger<RabbitMQEventbus>(new NullLoggerFactory());

            var functionProvider = new FunctionProvider();

            RabbitMQEventbus eventbus = new RabbitMQEventbus(config, connection,functionProvider,eventbusLogger);

            var message = "Tweet Partial";
            eventbus.Publish(new RabbitMQMessage(new MessageDestination("TweetsExchange", "tweets.create.partial"), message));

            Console.WriteLine("Numbers:");

            Console.ReadLine();
        }
    }
}
