using System.Reflection;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data;

public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
{
    public virtual DbSet<CatalogItem> CatalogItems { get; set; }
    public virtual DbSet<CatalogType> Types { get; set; }
    public virtual DbSet<CatalogBrand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
