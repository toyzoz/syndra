using Ordering.Domain.AggregateModels.Buyers.Events;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Buyers;

public class Buyer : AggregateRoot
{
    private readonly List<PaymentMethod> _paymentMethods = [];

    private Buyer()
    {
    }

    private Buyer(string identityGuid, string name)
    {
        IdentityGuid = identityGuid;
        Name = name;
    }

    public static Buyer CreateInstance(string identityGuid, string name)
    {
        return new Buyer(identityGuid, name);
    }

    public string IdentityGuid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    public PaymentMethod VerifyOrAddPaymentMethod(string alias, string cardNumber, string securityNumber,
        string cardHolderName, DateTime expiration, int cardTypeId)
    {
        var existingPayment =
            _paymentMethods.SingleOrDefault(pm => pm.IsEqualTo(cardTypeId, cardNumber, expiration));
        PaymentMethod? payment = PaymentMethod.Create(alias, cardNumber, securityNumber, cardHolderName, expiration, cardTypeId);
        if (existingPayment is not null) return existingPayment;

        _paymentMethods.Add(payment);
        AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, payment));
        return payment;
    }
}