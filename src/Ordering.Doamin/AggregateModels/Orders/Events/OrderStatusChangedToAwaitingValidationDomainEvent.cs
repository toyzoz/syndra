using System.Collections;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Orders.Events;

public class OrderStatusChangedToAwaitingValidationDomainEvent : IDomainEvent
{
    public OrderStatusChangedToAwaitingValidationDomainEvent(int id, IEnumerable orderItems)
    {
        throw new NotImplementedException();
    }
}