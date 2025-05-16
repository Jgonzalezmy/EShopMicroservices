using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.API.Models;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.Property(order => order.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                .IsRequired();

            builder.HasMany(order => order.OrderItems)
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId);

            builder.ComplexProperty(order => order.OrderName, nameBuilder =>
            {
                nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(order => order.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(address => address.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(address => address.LastName)
                               .HasMaxLength(50)
                               .IsRequired();

                addressBuilder.Property(address => address.EmailAddress)
               .HasMaxLength(50);

                addressBuilder.Property(address => address.AddressLine)
               .HasMaxLength(100)
               .IsRequired();

                addressBuilder.Property(address => address.Country)
               .HasMaxLength(50);

                addressBuilder.Property(address => address.State)
               .HasMaxLength(50);

                addressBuilder.Property(address => address.ZipCode)
               .HasMaxLength(5);

            });

            builder.ComplexProperty(order => order.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(address => address.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(address => address.LastName)
                               .HasMaxLength(50)
                               .IsRequired();

                addressBuilder.Property(address => address.EmailAddress)
               .HasMaxLength(50);

                addressBuilder.Property(address => address.AddressLine)
               .HasMaxLength(100)
               .IsRequired();

                addressBuilder.Property(address => address.Country)
               .HasMaxLength(50);

                addressBuilder.Property(address => address.State)
               .HasMaxLength(50);

                addressBuilder.Property(address => address.ZipCode)
               .HasMaxLength(5);

            });

            builder.ComplexProperty(order => order.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(payment => payment.CardName)
                .HasMaxLength(50);

                paymentBuilder.Property(payment => payment.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                paymentBuilder.Property(payment => payment.Expiration)
                .HasMaxLength(10);

                paymentBuilder.Property(payment => payment.CVV)
                .HasMaxLength(3);

                paymentBuilder.Property(payment => payment.PaymentMethod);

            });

            builder.Property(order => order.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(order => order.TotalPrice);


        }
    }
}
