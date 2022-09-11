using Gemma.Discount.GRPC.Entities;

namespace Gemma.Discount.GRPC.Repositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Coupon>> GetAllCoupons();
        Task<Coupon> GetCoupon(int id);
        Task<Coupon> GetCouponByProductName(string productName);
        Task<bool> CreateCoupon(Coupon request);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(int id);
    }
}
