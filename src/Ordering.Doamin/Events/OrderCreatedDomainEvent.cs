using Ordering.Domain.Orders;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Events
{
    public record OrderCreatedDomainEvent(Order Order) : IDomainEvent;

    public record OrderQueryDomainEvent(DateTime at) : IDomainEvent;
}