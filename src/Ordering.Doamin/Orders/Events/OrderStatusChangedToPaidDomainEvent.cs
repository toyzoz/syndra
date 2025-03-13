using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Orders.Events;

public class OrderStatusChangedToPaidDomainEvent : IDomainEvent
{
    public OrderStatusChangedToPaidDomainEvent(int id, IReadOnlyCollection<OrderItem> orderItems)
    {
        throw new NotImplementedException();
    }
}