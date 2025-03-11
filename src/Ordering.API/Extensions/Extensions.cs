using Ordering.Application.Orders;

namespace Ordering.API.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApiService(this IServiceCollection services)
        {
            services.AddScoped<OrderService>();

            return services;
        }
    }
}