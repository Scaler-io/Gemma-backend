using AutoMapper;
using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Application.Models.Dtos.Order;
using Gemma.Shared.Common;
using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using MediatR;
using Serilog;

namespace Gemma.Order.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, Result<List<OrdersDto>>>
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(ILogger logger, IOrderRepository orderRepository, IMapper mapper)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OrdersDto>>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            _logger.Here().MethodEnterd();

            var orderDetails = await _orderRepository.GetOrdersByUserName(request.UserName);

            if(orderDetails == null)
            {
                _logger.Here().Error($"{ErrorCodes.NotFound} No orders found for {request.UserName}");
                return Result<List<OrdersDto>>.Fail(ErrorCodes.NotFound);
            }

            var result = _mapper.Map<List<OrdersDto>>(orderDetails);

            _logger.Here().Information($"Orders found {orderDetails}");
            _logger.Here().MethodExited();

            return Result<List<OrdersDto>>.Success(result);
        }
    }
}
