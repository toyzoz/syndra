using Ordering.API.Controllers;
using Ordering.Application.Contract;
using Ordering.Application.Orders;
using Ordering.Infrastructure.Data;

namespace Ordering.API.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<OrderService>();

            services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<OrderContext>());
            return services;
        }

        public static void AddApplicationService1(this WebApplication app)
        {
           // using var sp = app.Services.CreateScope();
           //var context= sp.ServiceProvider.GetRequiredService<IApplicationContext>();

           // context.Database.EnsureCreated();
        }
    }
}