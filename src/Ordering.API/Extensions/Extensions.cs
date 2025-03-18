using Ordering.Application;
using Ordering.Application.Data;
using Ordering.Application.Orders;
using Ordering.Application.Queries;
using Ordering.Domain.AggregateModels.Buyers;
using Ordering.Domain.AggregateModels.Orders;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;
using Ordering.Infrastructure.Idempotency;
using Ordering.Infrastructure.Repositories;

namespace Ordering.API.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

        services.AddDatabase(configuration);

        services.AddScoped<IApplicationContext, OrderingContext>();
        services.AddScoped<OrderService>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        //services.AddScoped<IOrderQuery, OrderQuery>();
        services.AddScoped<IRequestManager, RequestManager>();

        return services;
    }
}