#pragma warning disable CS8618
using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CatalogGame> CatalogGames { get; set; }
    public DbSet<CatalogGenre> CatalogGenres { get; set; }
    public DbSet<CatalogPublisher> CatalogPublishers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CatalogGenreEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogGameEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogPublisherEntityTypeConfiguration());
    }
}
