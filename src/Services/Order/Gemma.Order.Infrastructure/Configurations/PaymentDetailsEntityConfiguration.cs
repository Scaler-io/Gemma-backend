using Gemma.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gemma.Order.Infrastructure.Configurations
{
    public class PaymentDetailsEntityConfiguration : IEntityTypeConfiguration<PaymentDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentDetails> builder)
        {
            builder.HasMany(pd => pd.Orders)
                   .WithOne(o => o.PaymentDetails)
                   .HasForeignKey(fk => fk.PaymentDetailsId);
        }
    }
}