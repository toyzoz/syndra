using Ordering.Application.Commands.Create;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.Commands.CreateDraft;

public record OrderDraftDto
{
    public decimal Total { get; init; }
    public required IEnumerable<OrderItemDto> OrderItems { get; init; }

    public static OrderDraftDto FromOrder(Order order) =>
        new()
        {
            Total = order.OrderItems.Sum(i => i.UnitPrice * i.Units),
            OrderItems = order.OrderItems.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Units = i.Units,
                PictureUrl = i.PictureUrl
            })
        };
}
