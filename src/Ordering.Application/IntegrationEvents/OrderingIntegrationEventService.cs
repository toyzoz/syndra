using EventBus;

namespace Ordering.Application.IntegrationEvents;

public class OrderingIntegrationEventService : IOrderingIntegrationEventService
{
    public Task AddEventAsync(IntegrationEvent @event)
    {
        throw new NotImplementedException();
    }

    public Task PublishThroughEventBusAsync(Guid transactionId)
    {
        throw new NotImplementedException();
    }
}