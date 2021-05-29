using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQEventbus.Extension.Connection;
using RabbitMQEventbus.Extension.Eventbus;
using RabbitMQEventbus.Extension.FunctionProvider;

namespace RabbitMQEventbus.Extension.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string configurationFileName = @"RabbitMQEventbus.json";
        private static readonly string configurationFileNameProduction = @"RabbitMQEventbus.Production.json";

        public static IServiceCollection AddRabbitMQEventbus(this IServiceCollection services, IWebHostEnvironment env)
        {
            var configFileName = env.IsProduction() ? configurationFileNameProduction : configurationFileName;
            if (!File.Exists(configFileName))
            {
                configFileName = configurationFileName;
            }
            
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, configFileName);
            var file = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<RabbitMQEventbusConfiguration.RabbitMQEventbusConfiguration>(file);

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
                return new Eventbus.RabbitMQEventbus(config,
                                            s.GetRequiredService<IPersistentConnection>(),
                                            s.GetRequiredService<IRabbitMQFunctionProvider>(),
                                            s.GetRequiredService<ILogger<Eventbus.RabbitMQEventbus>>());
            });

            return services;
        }
    }
}
