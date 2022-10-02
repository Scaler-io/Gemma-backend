namespace Gemma.Order.Application.Contracts.Persistance
{
    using Order = Order.Domain.Entities.Order;
    public interface IOrderRepository: IAsyncRepository<Order> 
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
