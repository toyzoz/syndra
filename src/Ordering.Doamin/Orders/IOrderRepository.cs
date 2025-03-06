using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Doamin.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetListAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<Order> DeleteAsync(Order order);
    }
}
