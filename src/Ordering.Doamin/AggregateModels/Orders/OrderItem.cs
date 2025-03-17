using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Orders;

public class OrderItem : Entity
{
    private OrderItem()
    {
    }

    public OrderItem(int productId,
        string productName,
        string productPicUrl,
        decimal unitPrice,
        int units,
        decimal discount)
    {
        ProductId = productId;
        ProductName = productName;
        PictureUrl = productPicUrl;
        UnitPrice = unitPrice;
        Units = units;
        Discount = discount;
    }

    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public string PictureUrl { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public int Units { get; private set; }

    internal void AddUnits(int units)
    {
        if (units < 0) throw new OrderingDomainException("Invalid unit");

        Units += units;
    }

    public void NewDiscount(decimal discount)
    {
        if (discount < 0) throw new OrderingDomainException("Invalid discount");

        Discount = discount;
    }
}