using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}