using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EnityConfigurations;

public class OrderGameEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderGame>
{
    public void Configure(EntityTypeBuilder<OrderGame> builder)
    {
        builder.ToTable("OrderGame");

        builder.Property(ci => ci.Id)
            .UseHiLo("order_game_hilo")
            .IsRequired();

        builder.Property(ci => ci.ProductId)
           .IsRequired(true);

        builder.Property(ci => ci.Name)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(ci => ci.Price)
            .IsRequired(true);

        builder.Property(ci => ci.Count)
           .IsRequired(true);

        builder.HasOne(ci => ci.OrderNumber)
            .WithMany()
            .HasForeignKey(ci => ci.OrderNumberId);
    }
}
