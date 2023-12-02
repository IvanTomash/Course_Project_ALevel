using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogGameEntityTypeConfiguration
: IEntityTypeConfiguration<CatalogGame>
{
    public void Configure(EntityTypeBuilder<CatalogGame> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_hilo")
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(ci => ci.Price)
            .IsRequired(true);

        builder.Property(ci => ci.PictureFileName)
            .IsRequired(false);

        builder.HasOne(ci => ci.CatalogGenre)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogGenreId);

        builder.HasOne(ci => ci.CatalogPublisher)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogPublisherId);
    }
}
