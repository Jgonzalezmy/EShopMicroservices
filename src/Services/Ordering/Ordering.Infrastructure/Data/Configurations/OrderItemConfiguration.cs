using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.API.Models;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(orderItem => orderItem.Id);

            builder.Property(orderItem => orderItem.Id).HasConversion(
                orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId));

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(orderItem => orderItem.ProductId);

            builder.Property(orderItem => orderItem.Quantity).IsRequired();
            builder.Property(orderItem => orderItem.Price).IsRequired();
        }
    }
}
