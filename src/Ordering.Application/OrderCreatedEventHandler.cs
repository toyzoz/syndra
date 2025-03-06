using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Doamin.Events
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedDomainEvent>
    {

        public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order created {id}", notification.newOrder.Id);
            return Task.CompletedTask;
        }
    }
}
