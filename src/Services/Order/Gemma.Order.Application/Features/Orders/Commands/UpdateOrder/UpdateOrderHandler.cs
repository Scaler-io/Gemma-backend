using AutoMapper;
using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Application.Models.Requests.OrderUpdate;
using Gemma.Shared.Common;
using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using MediatR;
using Serilog;

namespace Gemma.Order.Application.Features.Orders.Commands.UpdateOrder
{
    using Order = Order.Domain.Entities.Order;
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<bool>>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(ILogger logger, IMapper mapper, IOrderRepository orderRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;;
        }

        public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Here().MethodEnterd();

            var orderToUpdate = await _orderRepository.GetByIdAsync(request.UpdateOrder.OrderId);

            if(orderToUpdate == null)
            {
                _logger.Here().Error($"{ErrorCodes.NotFound} No order found with id {request.UpdateOrder.OrderId}");
                return Result<bool>.Fail(ErrorCodes.NotFound);
            }

            _mapper.Map(request.UpdateOrder, orderToUpdate, typeof(UpdateOrderRequest), typeof(Order));

            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.Here().Information($"Order {orderToUpdate.Id} is updated successfully");
            _logger.Here().MethodExited();

            return Result<bool>.Success(true);
        }
    }
}
