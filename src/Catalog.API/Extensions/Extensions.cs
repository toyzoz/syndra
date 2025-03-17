using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<CatalogContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("Database");
            options.UseSqlServer(connectionString);
        });

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        CatalogContext? context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
        await context.Database.MigrateAsync();
        await context.SeedAsync(app.Environment);
    }
}
