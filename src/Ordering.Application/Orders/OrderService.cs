using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Orders;


namespace Ordering.Application.Orders
{
    public class OrderService(IApplicationContext context)
    {
        public async Task<Order> CreateOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetListAsync()
        {
            List<Order> orders = await context.Orders.Include(oi => oi.OrderItems).ToListAsync();
            return orders;
        }
    }
}