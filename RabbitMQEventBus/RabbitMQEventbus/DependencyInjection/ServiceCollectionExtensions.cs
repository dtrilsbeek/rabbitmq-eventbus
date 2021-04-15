using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ_Eventbus.Configuration;
using RabbitMQ_Eventbus.Connection;
using RabbitMQ_Eventbus.Eventbus;
using RabbitMQ_Eventbus.FunctionProvider;
using System.IO;
using System.Reflection;

namespace RabbitMQ_Eventbus.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string configurationFileName = @"RabbitMQEventbus.json";

        public static IServiceCollection AddRabbitMQEventbus(this IServiceCollection services)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), configurationFileName);
            var file = File.ReadAllText(path);

            var config = JsonConvert.DeserializeObject<RabbitMQEventbusConfiguration>(file);

            services.AddSingleton<IConnectionFactory>(s =>
            {
                return new ConnectionFactory()
                {
                    HostName = config.Hostname,
                    UserName = config.Username,
                    Password = config.Password,
                    Port = config.Port
                };
            });

            services.AddSingleton<IPersistentConnection>(s =>
            {
                return new PersistentConnection(s.GetRequiredService<IConnectionFactory>(),
                                                s.GetRequiredService<ILogger<PersistentConnection>>());
            });

            services.AddSingleton<IRabbitMQEventbus>(s =>
            {
                return new RabbitMQEventbus(config,
                                            s.GetRequiredService<IPersistentConnection>(),
                                            s.GetRequiredService<IRabbitMQFunctionProvider>(),
                                            s.GetRequiredService<ILogger<RabbitMQEventbus>>());
            });

            return services;
        }
    }
}
