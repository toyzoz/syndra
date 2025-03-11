using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Data.EntityTypeConfigurations
{
    public class CatalogItemConfig : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Price).HasPrecision(18, 2);

            builder.HasOne(x => x.CatalogType).WithMany();
            builder.HasOne(x => x.CatalogBrand).WithMany();

            builder.HasIndex(x => x.Name);
        }
    }
}