using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Queries.ViewModels;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.Queries;

public class OrderQuery(IApplicationContext context) : IOrderQuery
{
    public async Task<List<CardTypeOutput>> GetCardTypesAsync() =>
        await context.CardTypes
            .Select(c => new CardTypeOutput(c.Id, c.Name))
            .OrderBy(c => c.Name)
            .ToListAsync();


    public async Task<Order> GetOrderByIdAsync(int id)
    {
        Order? order = await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with id {id} not found");
        }

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrderByUserAsync(string user) => throw new NotImplementedException();
}
