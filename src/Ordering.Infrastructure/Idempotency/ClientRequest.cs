namespace Ordering.Infrastructure.Idempotency;

public class ClientRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime DateTime { get; init; }
}
