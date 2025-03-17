using Microsoft.EntityFrameworkCore;
using Ordering.Domain.AggregateModels.Buyers;
using Ordering.Domain.AggregateModels.Buyers.Events;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class BuyerRepository(OrderingContext context) : IBuyerRepository
{
    public Buyer Add(Buyer buyer) => context.Buyers.Add(buyer).Entity;

    public Buyer Update(Buyer buyer) => context.Buyers.Update(buyer).Entity;

    public async Task<Buyer?> FindAsync(string identity) =>
        await context.Buyers
            .Include(b => b.PaymentMethods)
            .Where(b => b.IdentityGuid == identity)
            .SingleOrDefaultAsync();

    public async Task<Buyer?> FindByIdAsync(int id) =>
        await context.Buyers
            .Include(b => b.PaymentMethods)
            .Where(b => b.Id == id)
            .SingleOrDefaultAsync();
}
