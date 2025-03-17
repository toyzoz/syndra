using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Orders.Events;

public record OrderCancelledDomainEvent(Order Order) : IDomainEvent
{
}