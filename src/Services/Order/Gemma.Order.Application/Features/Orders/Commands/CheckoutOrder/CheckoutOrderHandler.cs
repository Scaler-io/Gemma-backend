using AutoMapper;
using Gemma.Order.Application.Contracts.Infrastructure;
using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Application.Models;
using Gemma.Shared.Common;
using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using MediatR;
using Serilog;

namespace Gemma.Order.Application.Features.Orders.Commands.CheckoutOrder
{
    using Order = Order.Domain.Entities.Order;
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, Result<int>>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;

        public CheckoutOrderHandler(ILogger logger, IMapper mapper, IOrderRepository orderRepository, IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _emailService = emailService;
        }

        public async Task<Result<int>> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Here().MethodEnterd();

            var orderEntity = _mapper.Map<Order>(request.CheckoutOrderRequest);
            var placedOrder = await _orderRepository.AddAsync(orderEntity);
    
            if(placedOrder == null)
            {
                _logger.Here().Error($"{ErrorCodes.Operationfailed} Failed to place order\n {orderEntity}");
                return Result<int>.Fail(ErrorCodes.Operationfailed);
            }

            await SendEmail(placedOrder);

            _logger.Here().Information($"Order created with order id {placedOrder.Id}");
            _logger.Here().MethodExited();
            return Result<int>.Success(placedOrder.Id);
        }
        
        private async Task SendEmail(Order order)
        {
            var email = new Email
            {
                To = "sharthakmallik@gmail.com",
                Subject = "Your order has been placed successfully",
                Body = $"See your order details . {order}"
            };

            await _emailService.SendEmail(email);    
        }   
    }
}
