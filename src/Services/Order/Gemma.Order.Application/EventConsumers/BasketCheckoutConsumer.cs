using AutoMapper;
using EventBus.Message.Events;
using Gemma.Order.Application.Features.Orders.Commands.CheckoutOrder;
using Gemma.Order.Application.Models.Requests.Checkout;
using Gemma.Shared.Extensions;
using MassTransit;
using MediatR;
using Serilog;

namespace Gemma.Order.Application.EventConsumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(ILogger logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            _logger.Here().MethodEnterd();
            var checkOrderRequest = _mapper.Map<CheckoutOrderRequest>(context.Message);
            var orderResponse = await _mediator.Send(new CheckoutOrderCommand { CheckoutOrderRequest = checkOrderRequest });

            _logger.Here().Information("Order placed successfully. {@newOrder}", orderResponse.Value);
            _logger.Here().MethodExited();
        }
    }
}
