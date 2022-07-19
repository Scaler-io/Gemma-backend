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
            
            CreateMap<ShoppingCart, BasketRequest>()
                .ForMember(s => s.UserName, o => o.MapFrom(d => d.UserName))
                .ForMember(s => s.Items, o => o.MapFrom(d => d.Items)).ReverseMap();

            CreateMap<ShoppingCartItem, ShoppingCartItemRequest>()
                .ForMember(s => s.ProductId, o => o.MapFrom(d => d.ProductId))
                .ForMember(s => s.Quantity, o => o.MapFrom(d => d.Quantity))
                .ForMember(s => s.ProductName, o => o.MapFrom(d => d.ProductName))
                .ForMember(s => s.Color, o => o.MapFrom(d => d.Color))
                .ForMember(s => s.Price, o => o.MapFrom(d => d.Price))
                .ReverseMap();

            CreateMap<ShoppingCartItem, ShoppingCartItemRequest>().ReverseMap();

            CreateMap<ShoppingCart, BasketResponse>()
                .ForMember(s => s.CreatedOn, o => o.MapFrom(d => d.CreatedOn))
                .ForMember(s => s.UpdatedOn, o => o.MapFrom(d => d.UpdatedOn))
                .ForMember(s => s.BasketTotal, o => o.MapFrom(d => d.TotalPrice)).ReverseMap();

            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>()
                .ReverseMap();
        }
    }
}
