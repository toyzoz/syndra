using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.AggregateModels.Buyers;
using Ordering.Domain.AggregateModels.Orders;
using Ordering.Infrastructure.Extensions;

namespace Ordering.Infrastructure.Data;

public class OrderingContext(
    DbContextOptions<OrderingContext> options,
    IMediator mediator)
    : DbContext(options), IApplicationContext
{
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CardType> CardTypes { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEventsAsync(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}