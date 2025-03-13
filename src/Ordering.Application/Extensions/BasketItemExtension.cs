using Ordering.Application.Commands.Create;
using Ordering.Application.Models;

namespace Ordering.Application.Extensions;

public static class BasketItemExtension
{
    public static OrderItemDto ToOrderItemDto(this BasketItem basketItem)
    {
        return new OrderItemDto
        {
            ProductId = basketItem.ProductId,
            ProductName = basketItem.ProductName,
            UnitPrice = basketItem.UnitPrice,

            Units = basketItem.Quantity,
            PictureUrl = basketItem.PictureUrl
        };
    }
}