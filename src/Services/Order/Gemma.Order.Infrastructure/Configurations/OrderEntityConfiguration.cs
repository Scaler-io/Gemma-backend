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
                .WithMany(ba => ba.Orders)
                .HasForeignKey(o => o.BillingAddressId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.PaymentDetails)
                .WithMany(pd => pd.Orders)
                .HasForeignKey(o => o.PaymentDetailsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
