using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Gemma.Order.Infrastructure.Repositories
{
    using Order = Order.Domain.Entities.Order;
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext): base(orderContext)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _orderContext.Orders.Where(order => order.UserName == userName).ToListAsync();
            return orderList;
        }
    }
}
