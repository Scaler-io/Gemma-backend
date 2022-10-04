using System.Linq.Expressions;
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
            var includes = new List<Expression<Func<Order, object>>>(){
                o => o.Address,
                o => o.PaymentDetails
            };
            var orderList = await GetAsync(o => o.UserName == userName, null, includes);
            return orderList;
        }
    }
}
