using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ_Eventbus.Connection;
using RabbitMQ_Eventbus.FunctionProvider;
using RabbitMQ_Eventbus.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ_Eventbus.Message;

namespace RabbitMQ_Eventbus.Eventbus
{
    public class RabbitMQEventbus : IRabbitMQEventbus
    {
        // queues cannot start with this string, these are reserved for internal use by RabbitMQ.
        private readonly string rabbitMQDefaultQueueNamingScheme = "amq.";

        public bool Configured { get; } = false;

        //public bool Connected => connection == null ? false : connection.IsConnected;
        public bool Connected => connection?.IsConnected ?? false;

        private IPersistentConnection connection;
        private ILogger<RabbitMQEventbus> logger;

        private IModel model;

        public RabbitMQEventbus(RabbitMQEventbusConfiguration configuration,
                                IPersistentConnection connection,
                                IRabbitMQFunctionProvider functionProvider,
                                ILogger<RabbitMQEventbus> logger)
        {
            this.connection = connection;
            this.logger = logger;

            if (!connection.TryConnect())
            {
                this.logger.LogError("Connecting to RabbitMQ broker failed.");
                return;
            }

            model = connection.CreateModel();

            if (!ConfigureEventbus(configuration, functionProvider))
            {
                this.logger.LogError("Configuring RabbitMQEventbus failed.");
            }

            Configured = true;
        }

        private bool ConfigureEventbus(RabbitMQEventbusConfiguration configuration,
                                       IRabbitMQFunctionProvider functionProvider)
        {
            var exchangesToDeclare = new List<Exchange>();
            var queuesToDeclare = new List<Queue>();
            var queueExchangeBindings = new List<QueueExchangeBinding>();

            foreach (var queue in configuration.Queues)
            {
                if (queue.Name.StartsWith(rabbitMQDefaultQueueNamingScheme))
                {
                    logger.LogWarning($"tried to create a queue named {queue.Name} but this is not allowed because it starts with {rabbitMQDefaultQueueNamingScheme}");
                    continue;
                }

                queuesToDeclare.Add(queue);
            }

            foreach (var exchange in configuration.Exchanges)
            {
                exchangesToDeclare.Add(exchange);
            }


            foreach (var subscription in configuration.Upstream)
            {
                // if there is no function, the subscription has no way t0 handle the incomming message
                // so it is logged and skipped.
                if (!functionProvider.HasFunction(subscription.FunctionKey))
                {
                    logger.LogWarning($"No function found with key {subscription.FunctionKey}");
                    continue;
                }

                QueueExchangeBinding binding = new QueueExchangeBinding();

             
                // if the queue already exists, it is retrieved, otherwise a new queue is made.
                if (subscription.QueueName != "" && queuesToDeclare.Exists(q => q.Name.Equals(subscription.QueueName)))
                {
                    binding.Queue = queuesToDeclare.FirstOrDefault(q => q.Name.Equals(subscription.QueueName));
                }
                else
                {
                    if (subscription.QueueName.StartsWith(rabbitMQDefaultQueueNamingScheme))
                    {
                        logger.LogWarning($"tried to create a queue named {subscription.QueueName} but this is not allowed because it starts with {rabbitMQDefaultQueueNamingScheme}");
                        continue;
                    }

                    binding.Queue = new Queue() { Name = subscription.QueueName };
                    queuesToDeclare.Add(binding.Queue);
                }

                // if the exchange already exists, it is retrieved, otherwise a new exchange is made.
                if (exchangesToDeclare.Exists(e => e.Name.Equals(subscription.Exchange)))
                {
                    binding.Exchange = exchangesToDeclare.FirstOrDefault(e => e.Name.Equals(subscription.Exchange));
                }
                else
                {
                    binding.Exchange = new Exchange { Name = subscription.Exchange };
                    exchangesToDeclare.Add(binding.Exchange);
                }

                binding.Route = subscription.Route;

                binding.FunctionKey = subscription.FunctionKey;
                binding.Function = functionProvider.GetFunction(subscription.FunctionKey);

                queueExchangeBindings.Add(binding);

                // if there is no downstream, skip to the next subscription
                if (subscription.Downstream == null)
                {
                    continue;
                }

                // declare the downstream exchange if it's not already going to be declared
                if (!exchangesToDeclare.Exists(e => e.Name.Equals(subscription.Downstream.Exchange)))
                {
                    exchangesToDeclare.Add(new Exchange { Name = subscription.Downstream.Exchange });
                }
                
                binding.Downstream = subscription.Downstream;
            }

            foreach (var queueToDeclare in queuesToDeclare)
            {
                // If the name is empty, RabbitMQ generates a name, this is then set as the queue name.
                queueToDeclare.Name = DeclareQueue(queueToDeclare);
            }

            foreach (var exchangeToDeclare in exchangesToDeclare)
            {
                DeclareExchange(exchangeToDeclare);
            }

            foreach (var queueExchangeBinding in queueExchangeBindings)
            {
                BindQueueToExchange(queueExchangeBinding);
                BindFunctionToQueue(queueExchangeBinding);
            }

            // For now just true is returned, this will change in the future when proper logging is implemented.
            return true;
        }

        private string DeclareQueue(Queue queue)
        {
            return model.QueueDeclare(queue: queue.Name, durable: queue.Durable, exclusive: queue.Exclusive, arguments: queue.Arguments).QueueName;
        }

        private void DeclareExchange(Exchange exchange)
        {
            model.ExchangeDeclare(exchange: exchange.Name, type: exchange.Type, durable: exchange.Durable, arguments: exchange.Arguments);
        }

        private void BindQueueToExchange(QueueExchangeBinding binding)
        {
            model.QueueBind(queue: binding.Queue.Name, exchange: binding.Exchange.Name, routingKey: binding.Route);
        }

        private void BindFunctionToQueue(QueueExchangeBinding binding)
        {
            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (model, ea) =>
            {
                var returnMessage = binding.Function(model, ea);

                if (returnMessage != null && binding.Downstream != null)
                {
                    Publish(new RabbitMQMessage(new MessageDestination(binding.Downstream.Exchange,
                                                                       binding.Downstream.Route),
                                                returnMessage.Message));
                }
            };

            model.BasicConsume(queue: binding.Queue.Name, autoAck: true, consumer: consumer);
        }

        public void Publish(RabbitMQMessage message)
        {
            var body = Encoding.UTF8.GetBytes(message.Message);

            model.BasicPublish(exchange: message.Destination.Exchange, routingKey: message.Destination.Route, null, body);
        }

        private class QueueExchangeBinding
        {
            public Queue Queue { get; set; }
            public Exchange Exchange { get; set; }
            public string Route { get; set; }
            public string FunctionKey { get; set; }
            public Func<object, BasicDeliverEventArgs, RabbitMQMessage> Function { get; set; }
            public Publication Downstream { get; set; }
        }
    }
}
