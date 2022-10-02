using Gemma.Order.Application.Models.Dtos.Order;
using Gemma.Shared.Common;
using MediatR;

namespace Gemma.Order.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery: IRequest<Result<List<OrdersDto>>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
