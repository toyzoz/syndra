using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application;
using Ordering.Application.Data;
using Ordering.Domain.SeedWork;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Idempotency;

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
        var aggregateEntries = context.ChangeTracker.Entries<AggregateRoot>();

        var hasEventEntity = aggregateEntries
            .Where(x => x.Entity.DomainEvents.Count != 0);

        var domainEvents = hasEventEntity
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        foreach (var item in hasEventEntity) item.Entity.ClearDomainEvent();

        foreach (var domainEvent in domainEvents)
        {
            Console.WriteLine($"{nameof(domainEvent)} published");
            await mediator.Publish(domainEvent);
        }
    }
}