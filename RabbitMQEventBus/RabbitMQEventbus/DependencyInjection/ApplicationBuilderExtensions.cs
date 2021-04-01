using Microsoft.AspNetCore.Builder;
using RabbitMQ_Eventbus.Eventbus;

namespace RabbitMQ_Eventbus.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMQEventBus(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.GetService(typeof(IRabbitMQEventbus));

            return builder;
        }
    }
}
