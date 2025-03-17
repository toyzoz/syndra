namespace Ordering.Domain.AggregateModels.Buyers;

public class PaymentMethod
{
    private PaymentMethod()
    {
    }

    private PaymentMethod(string alias, string cardNumber, string securityNumber, string cardHolderName,
        DateTime expiration, int cardTypeId)
    {
        if (expiration <= DateTime.UtcNow) throw new ArgumentException("Expiration date must be in the future.");

        if (string.IsNullOrWhiteSpace(alias)) throw new ArgumentException("Alias cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(cardNumber)) throw new ArgumentException("Card number cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(securityNumber))
            throw new ArgumentException("Security number cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(cardHolderName))
            throw new ArgumentException("Card holder name cannot be null or empty.");


        Alias = alias;
        CardNumber = cardNumber;
        SecurityNumber = securityNumber;
        CardHolderName = cardHolderName;
        Expiration = expiration;
        CardTypeId = cardTypeId;
    }

    // 别名
    private string Alias { get; set; } = null!;

    // 卡号
    private string CardNumber { get; } = null!;

    // 安全码
    private string SecurityNumber { get; set; } = null!;

    // 持卡人姓名
    private string CardHolderName { get; set; } = null!;

    // 到期时间
    private DateTime Expiration { get; }

    public int CardTypeId { get; set; }
    public CardType? CardType { get; set; }

    public static PaymentMethod Create(string alias, string cardNumber, string securityNumber,
        string cardHolderName, DateTime expiration, int cardTypeId)
    {
        return new PaymentMethod(alias, cardNumber, securityNumber, cardHolderName, expiration, cardTypeId);
    }

    public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
    {
        return CardTypeId == cardTypeId &&
               CardNumber == cardNumber &&
               Expiration == expiration;
    }
}