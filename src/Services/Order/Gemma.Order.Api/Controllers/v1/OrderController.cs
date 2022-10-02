using Gemma.Order.Application.Features.Orders.Commands.CheckoutOrder;
using Gemma.Order.Application.Features.Orders.Commands.DeleteOrder;
using Gemma.Order.Application.Features.Orders.Commands.UpdateOrder;
using Gemma.Order.Application.Features.Orders.Queries.GetOrdersList;
using Gemma.Order.Application.Models.Requests.Checkout;
using Gemma.Order.Application.Models.Requests.OrderDelete;
using Gemma.Order.Application.Models.Requests.OrderUpdate;
using Gemma.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Gemma.Order.Api.Controllers.v1
{
    [ApiVersion("1")]
    public class OrderController: BaseApiController
    {
        private readonly IMediator _mediator;
        public OrderController(ILogger logger, IMediator mediator)
            : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetOrders")]
        public async Task<IActionResult> GetOrdersByUserName([FromRoute] string username)
        {
            Logger.Here().MethodEnterd();
            var query = new GetOrdersListQuery(username);
            var result = await _mediator.Send(query);
            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpPost(Name = "CheckoutOrder")]
        public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderRequest request)
        {
            Logger.Here().MethodEnterd();
            var command = new CheckoutOrderCommand { CheckoutOrderRequest = request};
            var result = await _mediator.Send(command);
            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            Logger.Here().MethodEnterd();
            var command = new UpdateOrderCommand { UpdateOrder = request };
            var result = await _mediator.Send(command);
            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpDelete(Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderRequest request)
        {
            Logger.Here().MethodEnterd();
            var command = new DeleteOrderCommand {  DeleteOrder = request };
            var result = await _mediator.Send(command);
            Logger.Here().MethodExited();
            return OkOrFail(result);
        }
    }
}
