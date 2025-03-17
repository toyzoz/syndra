using EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventBusRabbitMQ;

public static class Extensions
{
    public static void AddRabbitMQEventBus(IHostApplicationBuilder builder)
    {
        builder.Configuration.GetSection(EventBusOptions.SectionName);
        builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
    }
}

public interface IConnectionManager
{
}
