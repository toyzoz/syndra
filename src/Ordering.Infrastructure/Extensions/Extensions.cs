using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using System.Runtime.CompilerServices;

namespace Ordering.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

           
            return services;
        }

        //public static void Migration(this WebApplication app)
        //{
        //    app.Services.CreateScope().ServiceProvider.GetRequiredService<OrderContext>().Database.Migrate();
        //}
    }


}