using AutoMapper;
using Gemma.Basket.API.Entities;
using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;

namespace Gemma.Basket.API.Mappers
{
    public class BasketMappingProfile: Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, BasketRequest>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemRequest>().ReverseMap();

            CreateMap<ShoppingCart, BasketResponse>()
                .ForMember(s => s.BasketTotal, o => o.MapFrom(d => d.TotalPrice)).ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartItemResponse>().ReverseMap();
        }
    }
}
