using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.API.Data
{
    public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogType> Types { get; set; }
        public DbSet<CatalogBrand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}