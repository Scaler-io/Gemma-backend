using Gemma.Order.Application.Models.Requests.OrderUpdate;
using Gemma.Shared.Common;
using MediatR;

namespace Gemma.Order.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand: IRequest<Result<bool>>
    {
        public UpdateOrderRequest UpdateOrder { get; set; }
    }
}
