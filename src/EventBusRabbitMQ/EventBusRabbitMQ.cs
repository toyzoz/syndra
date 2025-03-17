using System.Text;
using System.Text.Json;
using EventBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ;

public class EventBusRabbitMQ /*: IEventBus*/
{
    private readonly string exchangeName = "eshop_event_bus";
    public async Task PublishAsync(IntegrationEvent @event)
    {
        var routingKey = @event.GetGenericTypeName();
    
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        // exchange
        await channel.ExchangeDeclareAsync(exchange: exchangeName,
            type: ExchangeType.Direct);
    
    
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);
    
        await channel.BasicPublishAsync(exchange: exchangeName, routingKey: routingKey, body: body);
    }
    
    private async Task OnMessageRecive(object sender, BasicDeliverEventArgs eventArgs)
    {
        
    }
}