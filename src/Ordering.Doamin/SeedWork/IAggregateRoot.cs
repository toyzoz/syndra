namespace Ordering.Domain.SeedWork;

public interface IAggregateRoot
{
    void AddDomainEvent(IDomainEvent @event);
    void ClearDomainEvent();
}