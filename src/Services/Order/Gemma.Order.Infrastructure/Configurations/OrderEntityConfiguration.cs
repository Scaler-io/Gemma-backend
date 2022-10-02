using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gemma.Order.Infrastructure.Configurations
{
    using Order = Order.Domain.Entities.Order;

    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.BillingAddressId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.PaymentDetails)
                .WithMany()
                .HasForeignKey(o => o.PaymentDetailsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
