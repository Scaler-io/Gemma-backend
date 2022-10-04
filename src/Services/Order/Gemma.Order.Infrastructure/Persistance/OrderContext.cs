using Gemma.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gemma.Order.Infrastructure.Persistance
{
    using Order = Order.Domain.Entities.Order;
    public class OrderContext: DbContext
    {
        public OrderContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "default";
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "default";
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "default";
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
