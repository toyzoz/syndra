using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Queries;
using Ordering.Domain.Orders;

namespace Ordering.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            var v1 = typeof(Extensions).Assembly;
            var v2 = Assembly.GetExecutingAssembly();
            cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
        });

        services.AddScoped<IOrderQuery, OrderQuery>();
        return services;
    }
}