using MediatR;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.Cancel;

public class CancelOrderCommandHandler(IOrderRepository repository) : IRequestHandler<CancelOrderCommand, bool>
{
    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderNumber);

        if (order is null)
        {
            return false;
        }

        order.SetCancelledStatus();

        return true;
    }
}