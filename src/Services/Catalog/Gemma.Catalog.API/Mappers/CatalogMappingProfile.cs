using AutoMapper;
using Gemma.Catalog.API.Entities;
using Gemma.Catalog.API.Models.Requests;
using Gemma.Catalog.API.Models.Responses;

namespace Gemma.Catalog.API.Mappers
{
    public class CatalogMappingProfile: Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>()
                .ForMember(s => s.MetaData, o => o.MapFrom(d => new MetaData
                {
                    CreatedAt = d.CreatedOn,
                    UpdatedAt = d.UpdatedOn
                }));
        }
    }
}
