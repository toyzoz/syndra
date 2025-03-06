namespace Ordering.Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }

        public void ClearDomainEvent(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}