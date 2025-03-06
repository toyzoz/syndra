using Ordering.API.Controllers;
using Ordering.Application.Orders;
using Ordering.Infrastructure.Data;

namespace Ordering.API.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<OrderService>();
          
            return services;
        }
    }
}