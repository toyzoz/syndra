using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contract;
using Ordering.Domain.Orders;
using System.Reflection;

namespace Ordering.Infrastructure.Data
{
    public class OrderContext : DbContext, IApplicationContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}