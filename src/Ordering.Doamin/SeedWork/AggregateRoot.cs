namespace Ordering.Doamin.SeedWork
{
    public class AggregateRoot : IAggregateRoot
    {
        private List<IDomainEvent> _domainEvents = [];
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
