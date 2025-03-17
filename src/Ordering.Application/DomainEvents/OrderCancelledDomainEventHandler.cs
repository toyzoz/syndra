using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.Create;
using Ordering.Domain.AggregateModels.Buyers.Events;
using Ordering.Domain.AggregateModels.Orders;
using Ordering.Domain.AggregateModels.Orders.Events;

namespace Ordering.Application.DomainEvents;

public class OrderCancelledDomainEventHandler(
    ILogger<OrderCancelledDomainEventHandler> logger,
    IOrderRepository orderRepository,
    IBuyerRepository buyerRepository,
    IOrderingIntegrationEventService orderingIntegrationEventService
) : INotificationHandler<OrderCancelledDomainEvent>
{
    public async Task Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(notification.Order.Id);
        if (order == null) throw new ArgumentNullException(nameof(order));

        if (order.BuyerId == null) throw new ArgumentNullException(nameof(order.BuyerId));

        var buyer = await buyerRepository.FindByIdAsync(order.BuyerId.Value);
        if (buyer == null) throw new ArgumentNullException(nameof(buyer));

        await orderingIntegrationEventService.AddEventAsync(
            new OrderStatusChangedToCancelledIntegrationEvent(order.Id, order.OrderStatus, buyer.Name,
                buyer.IdentityGuid));
    }
}