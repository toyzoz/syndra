using Catalog.API.Models;
using System.Text.Json;

namespace Catalog.API.Data
{
    public static class CatalogContextDataSeed
    {
        public static async Task SeedAsync(this CatalogContext context, IWebHostEnvironment environment)
        {
            if (!context.CatalogItems.Any())
            {
                var environmentContentRootPath = environment.ContentRootPath;
                var sourcePath = Path.Combine(environmentContentRootPath, "Setup", "Catalog.json");
                var sourceJson = await File.ReadAllTextAsync(sourcePath);
                var catalogSourceEntry = JsonSerializer.Deserialize<List<CatalogSourceEntry>>(sourceJson)!;

                // Seed CatalogType
                context.Types.RemoveRange(context.Types);
                var types = catalogSourceEntry.Select(i => i.Type).Distinct()
                    .Select(t => new CatalogType { Type = t });
                await context.Types.AddRangeAsync(types);
                await context.SaveChangesAsync();
                Console.WriteLine("Catalog database seeded {context.Types.Count()}");

                // Seed CatalogBrand
                context.Brands.RemoveRange(context.Brands);
                var brands = catalogSourceEntry.Select(i => i.Brand).Distinct()
                    .Select(t => new CatalogBrand { Brand = t });
                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
                Console.WriteLine($"Catalog database seeded {context.Brands.Count()}");


                // Seed CatalogItem
                var typeDic = context.Types.ToDictionary(x => x.Type, x => x.Id);
                var brandDic = context.Brands.ToDictionary(x => x.Brand, x => x.Id);

                context.CatalogItems.RemoveRange(context.CatalogItems);
                var catalogItems = catalogSourceEntry.Select(i => new CatalogItem
                {
                    Id = 0,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    PictureFileName = $"{i.Id}.webp",
                    CatalogTypeId = typeDic[i.Type],
                    CatalogBrandId = brandDic[i.Brand],
                    AvailableStock = 100,
                    RestockThreshold = 100,
                    MaxStockThreshold = 200,
                });

                await context.CatalogItems.AddRangeAsync(catalogItems);
                await context.SaveChangesAsync();
                Console.WriteLine($"Catalog database seeded {context.CatalogItems.Count()}");
            }
        }


        private class CatalogSourceEntry(
            int id,
            string type,
            string brand,
            string name,
            string description,
            decimal price)
        {
            public int Id { get; set; } = id;
            public string Type { get; } = type;
            public string Brand { get; } = brand;
            public string Name { get; } = name;
            public string Description { get; } = description;
            public decimal Price { get; } = price;
        }
    }
}