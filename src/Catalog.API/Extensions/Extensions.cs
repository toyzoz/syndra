using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options =>
            {
                string? connectionString = configuration.GetConnectionString("Database");
                options.UseSqlServer(connectionString);
            });
        }

        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
            context.Database.Migrate();
            await CatalogContextDataSeed.SeedAsync(context);
        }
    }
}
