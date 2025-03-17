using EventBus;
using Ordering.Application.Extensions;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EventBusRabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    private readonly string exchangeName = "eshop_event_bus";

    public async Task PublishAsync(IntegrationEvent @event)
    {
        var routingKey = @event.GetGenericTypeName();

        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // 声明交换机
        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        // 发布消息
        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
    }
}