using AutoMapper;
using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Application.Models.Requests.OrderDelete;
using Gemma.Shared.Common;
using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using MediatR;
using Serilog;

namespace Gemma.Order.Application.Features.Orders.Commands.DeleteOrder
{
    using Order = Order.Domain.Entities.Order;
    public class DeleteOrderhandler : IRequestHandler<DeleteOrderCommand, Result<bool>>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderhandler(ILogger logger, IMapper mapper, IOrderRepository orderRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Here().MethodEnterd();

            var orderToDelete = await _orderRepository.GetByIdAsync(request.DeleteOrder.OrderId);

            if (orderToDelete == null)
            {
                _logger.Here().Error($"{ErrorCodes.NotFound} No order found with id {request.DeleteOrder.OrderId}");
                return Result<bool>.Fail(ErrorCodes.NotFound);
            }

            _mapper.Map(request.DeleteOrder, orderToDelete, typeof(DeleteOrderRequest), typeof(Order));

            await _orderRepository.DeleteAsync(orderToDelete);

            _logger.Here().Information($"Order deleted successfully");
            _logger.Here().MethodExited();

            return Result<bool>.Success(true);
        }
    }
}
