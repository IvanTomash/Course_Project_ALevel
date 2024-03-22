using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogGenreEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogGenre>
{
    public void Configure(EntityTypeBuilder<CatalogGenre> builder)
    {
        builder.ToTable("CatalogGenre");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_genre_hilo")
            .IsRequired();

        builder.Property(cb => cb.Genre)
            .IsRequired()
            .HasMaxLength(100);
    }
}
