using EventBus;

namespace Ordering.Application.Commands.Create;

public interface IOrderingIntegrationEventService
{
    Task PublishThroughEventBusAsync(Guid transactionId);
    Task AddEventAsync(IntegrationEvent @event);
}