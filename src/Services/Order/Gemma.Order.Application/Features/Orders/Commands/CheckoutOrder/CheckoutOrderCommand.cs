using Gemma.Order.Application.Models.Requests.Checkout;
using Gemma.Shared.Common;
using MediatR;

namespace Gemma.Order.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand: IRequest<Result<int>>
    {
        public CheckoutOrderRequest CheckoutOrderRequest { get; set; }
    }
}
