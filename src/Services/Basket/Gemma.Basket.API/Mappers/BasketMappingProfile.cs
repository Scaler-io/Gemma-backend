using AutoMapper;
using Gemma.Basket.API.Entities;
using Gemma.Basket.API.Models;
using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;

namespace Gemma.Basket.API.Mappers
{
    public class BasketMappingProfile: Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartRequest>()
                .ForMember(s => s.Items, o => o.MapFrom(d => d.Items)).ReverseMap();

            CreateMap<ShoppingCartItems, ShoppingCartItemRequest>()
                .ForMember(s => s.ProductId, o => o.MapFrom(d => d.ProductId))
               .ReverseMap();

            CreateMap<ShoppingCart, ShoppingCartResponse>()
                .ForMember(s => s.Id, o => o.MapFrom(d => d.Id.ToString()))
                .ForMember(s => s.Items, o => o.MapFrom(d => d.Items))
                .ForMember(s => s.Gross, o => o.MapFrom(d => d.TotalPrice))
                .ForMember(s => s.MetaData, o => o.MapFrom(d => new MetaData { CreatedAt = d.CreatedAt.ToString(), UpdatedAt = d.UpdatedAt.ToString() }));

            CreateMap<ShoppingCartItems, ShoppingCartItemResponse>()
                .ForMember(s => s.ProductId, o => o.MapFrom(d => d.ProductId));
        }
    }
}
