using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogPublisherEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogPublisher>
{
    public void Configure(EntityTypeBuilder<CatalogPublisher> builder)
    {
        builder.ToTable("CatalogPublisher");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_publisher_hilo")
            .IsRequired();

        builder.Property(cb => cb.Publisher)
            .IsRequired()
            .HasMaxLength(100);
    }
}