using AutoMapper;
using Gemma.Discount.GRPC.Entities;
using Gemma.Discount.GRPC.Protos;
using Gemma.Discount.GRPC.Repositories;
using Gemma.Shared.Extensions;
using Grpc.Core;
using ILogger = Serilog.ILogger;

namespace Gemma.Discount.GRPC.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscountById(GetDiscountByIdRequest request, ServerCallContext context)
        {
            _logger.Here().MethodEnterd();

            var coupon = await _discountRepository.GetCoupon(request.ProductId);

            if (coupon == null)
            {
                _logger.Here().Warning("No coupon found by @Id", request.ProductId);
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productId {request.ProductId} was not found."));
            }

            _logger.Here().Information("Discount is retrieved. @coupon", coupon);

            var couponResult = _mapper.Map<CouponModel>(coupon);
            _logger.Here().MethodExited();

            return couponResult;
        }

        public override async Task<CouponModel> GetDiscountByProductName(GetDiscountByProductNameRequest request, ServerCallContext context)
        {
            _logger.Here().MethodEnterd();

            var coupon = await _discountRepository.GetCouponByProductName(request.ProductName);

            if (coupon == null)
            {
                _logger.Here().Warning("No coupon found by {@prodctName}", request.ProductName);
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productName {request.ProductName} was not found."));
            }

            _logger.Here().Information("Discount is retrieved. {@coupon}", coupon);

            var couponResult = _mapper.Map<CouponModel>(coupon);
            _logger.Here().MethodExited();

            return couponResult;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            _logger.Here().MethodEnterd();

            if (DiscountAlreadyExists(request))
            {
                _logger.Here().Information("The discount coupon with product name @ProductName already exists.", request.Coupon.ProductName);
                throw new RpcException(new Status(StatusCode.AlreadyExists, $"The discount coupon with product name {request.Coupon.ProductName} already exists."));
            }

            var couponRequest = _mapper.Map<Coupon>(request.Coupon);

            var isCouponCreated = await _discountRepository.CreateCoupon(couponRequest);

            if (!isCouponCreated)
            {
                _logger.Here().Error("Failed to create discount. @Payload", request.Coupon);
                throw new RpcException(new Status(StatusCode.Internal, "Failed to create discount"));
            }

            _logger.Here().Information("Discount coupon created successfully. @Payload", request.Coupon);
            _logger.Here().MethodExited();

            return request.Coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            _logger.Here().MethodEnterd();

            var couponRequest = _mapper.Map<Coupon>(request.Coupon);

            var couponUpdated = await _discountRepository.UpdateCoupon(couponRequest);

            if (!couponUpdated)
            {
                _logger.Here().Error("Failed to update discount. @Payload", request.Coupon);
                throw new RpcException(new Status(StatusCode.Internal, "Failed to update discount"));
            }

            _logger.Here().Information("Discount coupon updated successfully. @Payload", request.Coupon);
            _logger.Here().MethodExited();

            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            _logger.Here().MethodExited();

            var couponDeleted = await _discountRepository.DeleteCoupon(request.Id);

            if (!couponDeleted)
            {
                _logger.Here().Error("Failed to delete discount. @Id", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, "Failed to delete discount"));
            }

            var deleteCouponResonse = new DeleteDiscountResponse
            {
                Success = couponDeleted
            };

            _logger.Here().Information("Discount coupon deleted successfully. @Id", request.Id);
            _logger.Here().MethodExited();

            return deleteCouponResonse;
        }

        private bool DiscountAlreadyExists(CreateDiscountRequest request)
        {
            var coupon = _discountRepository.GetCouponByProductName(request.Coupon.ProductName);
            if (coupon != null) return true;
            return false;
        }
    }
}

