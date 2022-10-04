using Gemma.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gemma.Order.Infrastructure.Configurations
{
    public class BillingAddressEntityConfiguration : IEntityTypeConfiguration<BillingAddress>
    {
        public void Configure(EntityTypeBuilder<BillingAddress> builder)
        {
            builder.HasMany(ba => ba.Orders)
                    .WithOne(o => o.Address)
                    .HasForeignKey(fk => fk.BillingAddressId);                    
        }
    }
}
