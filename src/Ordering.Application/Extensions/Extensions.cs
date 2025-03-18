using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Commands.Cancel;
using Ordering.Application.Commands.Create;
using Ordering.Application.Commands.Ship;
using Ordering.Application.IntegrationEvents;
using Ordering.Application.Queries;
using Ordering.Application.Validations;

namespace Ordering.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        // MediatR
        services.AddMediatR(cfg =>
        {
            var v1 = typeof(Extensions).Assembly;
            var v2 = Assembly.GetExecutingAssembly();
            cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
        });

        // services
        services.AddScoped<IOrderQuery, OrderQuery>();
        services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

        // command validators
        services.AddSingleton<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
        services.AddSingleton<IValidator<CancelOrderCommand>, CancelOrderCommandValidator>();
        services.AddSingleton<IValidator<ShipOrderCommand>, ShipOrderCommandValidator>();

        return services;
    }
}

