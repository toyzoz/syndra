using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.SeedWork;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderingContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
        return services;
    }

    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OrderingContext>();
        await context.Database.MigrateAsync();
    }


    public static async Task DispatchDomainEventsAsync(this IMediator mediator,
        OrderingContext context)
    {
        IEnumerable<EntityEntry<AggregateRoot>>? aggregateEntries = context.ChangeTracker.Entries<AggregateRoot>();

        IEnumerable<EntityEntry<AggregateRoot>>? hasEventEntity = aggregateEntries
            .Where(x => x.Entity.DomainEvents.Count != 0);

        List<IDomainEvent>? domainEvents = hasEventEntity
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        foreach (EntityEntry<AggregateRoot>? item in hasEventEntity) item.Entity.ClearDomainEvent();

        foreach (var domainEvent in domainEvents)
        {
            Console.WriteLine($"{nameof(domainEvent)} published");
            await mediator.Publish(domainEvent);
        }
    }
}