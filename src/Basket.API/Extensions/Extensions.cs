using Basket.API.Repositories;
using Basket.API.Services;
using StackExchange.Redis;

namespace Basket.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder app)
    {
        var redis = app.Configuration.GetConnectionString("Redis") ??
                    throw new ArgumentNullException("basket.api");
        ConnectionMultiplexer.Connect(redis);
        app.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redis));

        app.Services.AddScoped<IUserIdentityService, UserIdentityService>();
        app.Services.AddScoped<IBasketRepository, RedisBasketRepository>();
    }
}