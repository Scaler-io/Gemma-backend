using AutoMapper;
using Gemma.Catalog.API.Entities;
using Gemma.Catalog.API.Models;

namespace Gemma.Catalog.API.Mappers
{
    public class CatalogMappingProfile: Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<ProductRequest, Product>();     
        }
    }
}
