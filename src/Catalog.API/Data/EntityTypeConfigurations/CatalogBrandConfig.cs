using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Data.EntityTypeConfigurations
{
    public class CatalogBrandConfig : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Brand).HasMaxLength(50);
        }
    }
}