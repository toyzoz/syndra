using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Orders;

namespace Ordering.Application.Data;

public interface IApplicationContext
{
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}