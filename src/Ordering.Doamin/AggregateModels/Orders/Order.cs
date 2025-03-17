using Ordering.Domain.AggregateModels.Orders.Events;
using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregateModels.Orders;

public class Order : AggregateRoot
{
    private readonly List<OrderItem> _orderItems = [];
    private bool _isDraft;

    private Order()
    {
    }

    private Order(Address address, string buyerId)
    {
        Address = address;
    }

    public int? BuyerId { get; private set; }
    public Address Address { get; private set; } = null!;
    public DateTime OrderDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public string Description { get; private set; } = null!;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public static Order Create(Address address, string buyerId)
    {
        return new Order(address, buyerId);
    }

    public static Order NewDraft()
    {
        Order? order = new() { _isDraft = true };
        return order;
    }

    public void AddOrderItem(int productId, string productName, string productPicUrl, decimal unitPrice, int units,
        decimal discount)
    {
        var existingProduct = _orderItems.SingleOrDefault(oi => oi.ProductId == productId);
        if (existingProduct is null)
        {
            var orderItem = OrderItem.Create(productId, productName, productPicUrl, unitPrice, units, discount);
            _orderItems.Add(orderItem);
        }
        else
        {
            existingProduct.AddUnits(units);
        }
    }

    public void SetAwaitingValidationStatus()
    {
        if (OrderStatus != OrderStatus.Submitted) return;

        AddDomainEvent(new OrderStatusChangedToAwaitingValidationDomainEvent(Id, _orderItems));
        OrderStatus = OrderStatus.AwaitingValidation;
        Description = "The order is awaiting validation.";
    }

    public void SetStockConfirmedStatus()
    {
        if (OrderStatus != OrderStatus.AwaitingValidation) return;

        AddDomainEvent(new OrderStatusChangedToStockConfirmedDomainEvent(Id));
        OrderStatus = OrderStatus.StockConfirmed;
        Description = "All the items were confirmed with available stock.";
    }

    public void SetPaidStatus()
    {
        if (OrderStatus != OrderStatus.StockConfirmed) return;

        AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id, OrderItems));
        OrderStatus = OrderStatus.Paid;
        Description =
            "The payment was performed at a simulated \"American Bank checking bank account ending on XX35071\"";
    }

    public void SetShippedStatus()
    {
        if (OrderStatus != OrderStatus.Paid) StatusChangeException(OrderStatus.Shipped);

        OrderStatus = OrderStatus.Shipped;
        Description = "The order was shipped.";
        AddDomainEvent(new OrderShippedDomainEvent(this));
    }

    private void StatusChangeException(OrderStatus orderStatusToChange)
    {
        throw new OrderingDomainException(
            $"Is not possible to change the order status from {OrderStatus} to {orderStatusToChange}.");
    }

    public void SetCancelledStatus()
    {
        if (OrderStatus is OrderStatus.Paid or OrderStatus.Shipped) StatusChangeException(OrderStatus.Cancelled);

        OrderStatus = OrderStatus.Cancelled;
        Description = "The order was cancelled.";
        AddDomainEvent(new OrderCancelledDomainEvent(this));
    }
}