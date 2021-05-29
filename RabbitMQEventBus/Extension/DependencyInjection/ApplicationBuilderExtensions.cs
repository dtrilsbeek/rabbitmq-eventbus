using Microsoft.AspNetCore.Builder;
using RabbitMQEventbus.Extension.Eventbus;

namespace RabbitMQEventbus.Extension.DependencyInjection
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
