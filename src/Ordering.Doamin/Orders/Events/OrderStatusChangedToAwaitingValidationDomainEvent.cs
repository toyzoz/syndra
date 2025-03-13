using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Orders.Events;

public class OrderStatusChangedToAwaitingValidationDomainEvent : IDomainEvent
{
    public OrderStatusChangedToAwaitingValidationDomainEvent(int id, List<OrderItem> orderItems)
    {
        throw new NotImplementedException();
    }
}