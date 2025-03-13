using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Orders.Events;

public record OrderCreatedDomainEvent(Order Order) : IDomainEvent;