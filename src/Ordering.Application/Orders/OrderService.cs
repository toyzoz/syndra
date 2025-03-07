using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Orders;


namespace Ordering.Application.Orders
{
    public class OrderService(IApplicationContext context)
    {
        public async Task<Order> CreateOrderAsync(Order order)
        {
           var newOrder= Order.Create(order.Description);
            foreach (var item in order.OrderItems)
            {
                newOrder.AddItem(item.ProductId, item.ProductName, item.PictureUrl,
                    item.UnitPrice, item.Units);
            }
            context.Orders.Add(newOrder);
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