namespace EventBus;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}