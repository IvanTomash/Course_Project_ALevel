using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EnityConfigurations;

public class OrderNumberEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderNumber>
{
    public void Configure(EntityTypeBuilder<OrderNumber> builder)
    {
        builder.ToTable("OrderNumber");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("order_number_hilo")
            .IsRequired();

        builder.Property(cb => cb.Number)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cb => cb.PersonId)
            .IsRequired()
            .HasMaxLength(50);
    }
}
