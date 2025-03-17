using EventBus;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.DomainEvents;

public record OrderStatusChangedToCancelledIntegrationEvent(
    int OrderId,
    OrderStatus OrderStatus,
    string BuyerName,
    string BuyerIdentityGuid) : IntegrationEvent;