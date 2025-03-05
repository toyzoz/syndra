﻿using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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

        public static async Task UseDataSeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
            context.Database.Migrate();
            await CatalogContextDataSeed.SeedAsync(context);
        }
    }
}
