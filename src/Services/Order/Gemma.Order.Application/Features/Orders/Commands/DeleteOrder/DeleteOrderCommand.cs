using Gemma.Order.Application.Models.Requests.OrderDelete;
using Gemma.Shared.Common;
using MediatR;

namespace Gemma.Order.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand: IRequest<Result<bool>>
    {
        public DeleteOrderRequest DeleteOrder { get; set; }
    }
}
