using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Orders.Events;

namespace Ordering.Application;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Order created {id}", notification.Order.Id);
        return Task.CompletedTask;
    }
}