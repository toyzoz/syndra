using System.Text;
using System.Text.Json;
using EventBus;
using Ordering.Application.Extensions;
using RabbitMQ.Client;

namespace EventBusRabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    private readonly string exchangeName = "eshop_event_bus";

    public async Task PublishAsync(IntegrationEvent @event)
    {
        string routingKey = @event.GetGenericTypeName();

        ConnectionFactory factory = new() { HostName = "localhost" };
        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();
        // exchange
        await channel.ExchangeDeclareAsync(exchangeName,
            ExchangeType.Direct);


        string message = JsonSerializer.Serialize(@event);
        byte[] body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchangeName, routingKey, body);
    }
}
