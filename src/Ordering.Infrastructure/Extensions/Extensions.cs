using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


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


            services.AddScoped<IApplicationContext, OrderContext>();

            return services;
        }

        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<OrderContext>();
            await context.Database.MigrateAsync();
        }
    }


}