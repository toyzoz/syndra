namespace Ordering.Domain.Orders;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetListAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order> AddAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task<Order> RemoveAsync(Order order);
}