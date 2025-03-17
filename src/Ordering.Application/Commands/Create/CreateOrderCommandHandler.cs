using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.Commands.Create;

public class CreateOrderCommandHandler(
    IOrderRepository repository,
    IOrderingIntegrationEventService integrationEventService,
    ILogger<CreateOrderCommandHandler> logger)
    : IRequestHandler<CreateOrderCommand, bool>
{
    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderStartedIntegrationEvent? orderStartedIntegrationEvent = new(request.UserId);
        await integrationEventService.AddEventAsync(orderStartedIntegrationEvent);

        Address address = new(request.Street, request.City, request.State, request.Country, request.ZipCode);

        Order order = new(address, request.UserId);

        foreach (var item in request.Items)
            order.AddOrderItem(item.ProductId, item.ProductName, item.PictureUrl, item.UnitPrice, item.Units,
                item.Discount);

        logger.LogInformation("create order -order: {order}", order);
        await repository.AddAsync(order);

        return true;
    }
}