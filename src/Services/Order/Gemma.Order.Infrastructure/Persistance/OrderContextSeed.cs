using Gemma.Order.Domain.Entities;
using Serilog;

namespace Gemma.Order.Infrastructure.Persistance
{
    using Order = Order.Domain.Entities.Order;

    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();   
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "sharthak95",
                    Address = new BillingAddress
                    {
                        FirstName = "Sharthak",
                        LastName =  "Mallik",
                        EmailAddress = "sharthakmallik@email.com",
                        AddressLine = "paticolony, road no3, p.o pradhan nagar, Darjeeling",
                        State = "West Bengal",
                        ZipCode = "734003",
                        Country = "India"
                    },
                    PaymentDetails = new PaymentDetails
                    {
                        CardName = "Sharthak Mallik",
                        CardNumber = "123456",
                        CVV =  "123",
                        Expiration = DateTime.Now.AddYears(10).ToString("dd/MM/yyyy"),
                        PaymentMethod = 1
                    }
                }
            };
        }
    }
}
