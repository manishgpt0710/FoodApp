using AutoMapper;
using FoodApp.Services.ProductAPI.Models;
using FoodApp.Services.ProductAPI.Models.Dto;

namespace FoodApp.Services.ProductAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
