using AutoMapper;
using Gemma.Discount.GRPC.Entities;
using Gemma.Discount.GRPC.Protos;

namespace Gemma.Discount.GRPC.Mappers
{
    public class DiscountMappingProfile: Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
