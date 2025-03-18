using EventBus;

namespace Ordering.Application.IntegrationEvents;

public interface IOrderingIntegrationEventService
{
    Task PublishThroughEventBusAsync(Guid transactionId);
    Task AddEventAsync(IntegrationEvent @event);
}
