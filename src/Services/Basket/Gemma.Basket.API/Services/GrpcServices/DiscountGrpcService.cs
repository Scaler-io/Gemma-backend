using Gemma.Discount.GRPC.Protos;
using Gemma.Shared.Common;
using Gemma.Shared.Extensions;
using ILogger = Serilog.ILogger;

namespace Gemma.Basket.API.Services.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;
        private readonly ILogger _logger;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient, ILogger logger)
        {
            _grpcClient = grpcClient;
            _logger = logger;
        }

        public async Task<CouponModel> GetDiscountCoupon(string productId) 
        {
            _logger.Here().MethodEnterd();
            var discountRequest = new GetDiscountByIdRequest { ProductId = productId };

            _logger.Here().MethodExited();
            return await _grpcClient.GetDiscountByIdAsync(discountRequest);
        } 
    }
}
