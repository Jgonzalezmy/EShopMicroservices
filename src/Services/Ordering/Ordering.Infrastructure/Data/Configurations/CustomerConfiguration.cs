using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.API.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(customer => customer.Id);

            builder.Property(customer => customer.Id).HasConversion(
                customerId => customerId.Value,
                dbId => CustomerId.Of(dbId));

            builder.Property(customer => customer.Name).HasMaxLength(100).IsRequired();
            builder.Property(customer => customer.Email).HasMaxLength(255);
            builder.HasIndex(customer => customer.Email).IsUnique();
        }
    }
}
