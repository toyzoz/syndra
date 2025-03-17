using System.Text;
using System.Text.Json;
using EventBus;
using RabbitMQ.Client;

namespace EventBusRabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    private const string ExchangeName = "eshop_event_bus";

    public async Task PublishAsync(IntegrationEvent @event)
    {
        var routingKey = @event.GetGenericTypeName();

        ConnectionFactory factory = new() { HostName = "localhost" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        // exchange
        await channel.ExchangeDeclareAsync(ExchangeName,
            ExchangeType.Direct);


        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(ExchangeName, routingKey, body);
    }
}