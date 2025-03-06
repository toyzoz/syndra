using Catalog.API.Models;

namespace Catalog.API.Data
{
    public static class CatalogContextDataSeed
    {
        public static async Task SeedAsync(this CatalogContext context)
        {
            if (!context.Products.Any())
            {
                List<Product> products =
                [
                    new() {  Name = "Product 1", Description = "Description 1" },
                    new() {  Name = "Product 2", Description = "Description 2" },
                    new() {  Name = "Product 3", Description = "Description 3" }
                ];
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}