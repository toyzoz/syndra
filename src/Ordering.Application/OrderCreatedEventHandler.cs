using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application
{
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

    public class OrderQueryDomainEventHandler(ILogger<OrderQueryDomainEventHandler> logger)
        : INotificationHandler<OrderQueryDomainEvent>
    {
        public Task Handle(OrderQueryDomainEvent notification, CancellationToken cancellationToken)
        {
            logger.LogError("ohhhhhh...");
            return Task.CompletedTask;
        }
    }
}