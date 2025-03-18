using System.Text;
using System.Text.Json;
using EventBus;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ;

public class EventBusRabbitMQ(ILogger<EventBusRabbitMQ> logger,
IRabbitMQPersistentConnection persistentConnection)
: IEventBus
{
    private const string exchangeName = "eshop_event_bus";
    private IChannel ConsumerChannel;
    public async Task PublishAsync(IntegrationEvent @event)
    {
        if (!persistentConnection.IsConnected)
        {
            persistentConnection.TryConnect();
        }

        using var channel = await persistentConnection.CreateChannleAsync();
        await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct);

        var body = JsonSerializer.SerializeToUtf8Bytes(@event);
        // todo 持久化
        await channel.BasicPublishAsync(
           exchange: exchangeName,
           routingKey: @event.GetType().Name,
           body: body);
    }

    private async Task StartBasicConsume()
    {
        logger.LogInformation("EventBusRabbitMQ StartBasicConsume");
        if (!persistentConnection.IsConnected)
        {
            persistentConnection.TryConnect();
        }

        using var channel = await persistentConnection.CreateChannleAsync();
        await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct);

        var queueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var eventName = ea.RoutingKey;
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            await ProcessEvent(eventName, message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    private async Task CreateConsumerChannel()
    {
        if (!persistentConnection.IsConnected)
        {
            persistentConnection.TryConnect();
        }

        var channel = await persistentConnection.CreateChannleAsync();

        await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct);

        await channel.QueueDeclareAsync(queue: "eshop_event_bus", durable: true, exclusive: false, autoDelete: false, arguments: null);
        await channel.QueueBindAsync(queue: "eshop_event_bus", exchange: exchangeName, routingKey: "ProductPriceChangedIntegrationEvent");


    }
}