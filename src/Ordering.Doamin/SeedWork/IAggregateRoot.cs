namespace Ordering.Doamin.SeedWork
{
    public interface IAggregateRoot
    {
        void AddDomainEvent(IDomainEvent @event);
        void ClearDomainEvent(IDomainEvent @event);
    }
}
