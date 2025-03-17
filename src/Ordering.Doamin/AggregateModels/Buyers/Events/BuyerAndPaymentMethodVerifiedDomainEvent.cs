using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Buyers.Events;

public record BuyerAndPaymentMethodVerifiedDomainEvent(Buyer Buyer, PaymentMethod Payment) : IDomainEvent
{
}
