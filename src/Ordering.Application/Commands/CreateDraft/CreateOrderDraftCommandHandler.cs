using MediatR;
using Ordering.Application.Commands.Create;
using Ordering.Application.Extensions;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.Commands.CreateDraft;

public class CreateOrderDraftCommandHandler
    : IRequestHandler<CreateOrderDraftCommand, OrderDraftDto>
{
    public Task<OrderDraftDto> Handle(CreateOrderDraftCommand request, CancellationToken cancellationToken)
    {
        var draftOrder = Order.NewDraft();
        List<OrderItemDto>? orderItemDtos = request.Items.Select(i => i.ToOrderItemDto()).ToList();

        foreach (var item in orderItemDtos)
            draftOrder.AddOrderItem(
                item.ProductId,
                item.ProductName,
                item.PictureUrl,
                item.UnitPrice,
                item.Units,
                item.Discount);

        return Task.FromResult(OrderDraftDto.FromOrder(draftOrder));
    }
}