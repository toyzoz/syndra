using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Buyers.Events;

public record BuyerAndPaymentMethodVerifiedDomainEvent(Buyer Buyer, PaymentMethod Payment) : IDomainEvent
{
}