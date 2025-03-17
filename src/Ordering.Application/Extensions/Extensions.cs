﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Queries;

namespace Ordering.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            Assembly? v1 = typeof(Extensions).Assembly;
            Assembly? v2 = Assembly.GetExecutingAssembly();
            cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
        });

        services.AddScoped<IOrderQuery, OrderQuery>();
        return services;
    }
}
