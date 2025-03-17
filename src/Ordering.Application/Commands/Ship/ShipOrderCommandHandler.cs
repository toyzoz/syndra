using MediatR;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.Commands.Ship;

public class ShipOrderCommandHandler(IOrderRepository repository) : IRequestHandler<ShipOrderCommand, bool>
{
    public async Task<bool> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await repository.GetByIdAsync(request.OrderNumber);

        if (order is null)
        {
            return false;
        }

        order.SetShippedStatus();

        return true;
    }
}
