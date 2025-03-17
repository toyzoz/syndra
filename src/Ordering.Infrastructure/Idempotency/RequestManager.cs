using Ordering.Application;
using Ordering.Domain.Exceptions;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Idempotency;

public class RequestManager(OrderingContext context) : IRequestManager
{
    public async Task<bool> ExistAsync(Guid id)
    {
        var clientRequest = await context.FindAsync<ClientRequest>(id);
        return clientRequest != null;
    }

    public async Task CreateRequestForCommandAsync<T>(Guid id)
    {
        if (await ExistAsync(id)) throw new OrderingDomainException($"Request {id} already exists.");

        ClientRequest clientRequest = new() { Id = id, Name = typeof(T).Name, DateTime = DateTime.UtcNow };

        await context.AddAsync(clientRequest);
        await context.SaveChangesAsync();
    }
}