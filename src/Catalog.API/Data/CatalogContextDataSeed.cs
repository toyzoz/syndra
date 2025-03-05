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
                    new Product() { Id = 1, Name = "Product 1", Description = "Description 1", },
                    new Product() { Id = 2, Name = "Product 2", Description = "Description 2", },
                    new Product() { Id = 3, Name = "Product 3", Description = "Description 3", }
                ];
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}