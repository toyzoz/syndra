using Microsoft.EntityFrameworkCore;
using Ordering.Domain.AggregateModels.Orders;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderingContext context) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetListAsync()
    {
        List<Order>? orders = await context.Orders.ToListAsync();

        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id) =>
        await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<Order> AddAsync(Order order) => (await context.Orders.AddAsync(order)).Entity;
}
