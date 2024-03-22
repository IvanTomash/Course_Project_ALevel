#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using Order.Host.Data.EnityConfigurations;
using Order.Host.Data.Entities;

namespace Order.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderGame> OrderGames { get; set; }
    public DbSet<OrderNumber> OrderNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderNumberEntityTypeConfiguration());
        builder.ApplyConfiguration(new OrderGameEntityTypeConfiguration());
    }
}
