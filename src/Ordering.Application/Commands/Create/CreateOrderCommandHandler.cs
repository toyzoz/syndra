using MediatR;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.Create;

public class CreateOrderCommandHandler(IOrderRepository repository) : IRequestHandler<CreateOrderCommand, bool>
{
    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // new order

        // clear basket

        var address = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);
        var order = new Order(address);

        foreach (var item in request.Items)
            order.AddItem(item.ProductId, item.ProductName, item.PictureUrl, item.UnitPrice, item.Units, item.Discount);

        await repository.AddAsync(order);

        return true;
    }
}