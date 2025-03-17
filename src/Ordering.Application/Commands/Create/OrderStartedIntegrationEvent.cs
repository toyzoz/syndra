using EventBus;

namespace Ordering.Application.Commands.Create;

public record OrderStartedIntegrationEvent(string UserId) : IntegrationEvent
{
    public string UserId { get; set; } = UserId;
}