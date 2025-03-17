using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Orders.Events;

public class OrderShippedDomainEvent : IDomainEvent
{
    public OrderShippedDomainEvent(Order order) => throw new NotImplementedException();
}
