using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Orders.Events;

public record OrderCreatedDomainEvent(Order Order) : IDomainEvent;