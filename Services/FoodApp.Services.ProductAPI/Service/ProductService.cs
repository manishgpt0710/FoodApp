using AutoMapper;
using FoodApp.Services.ProductAPI.Data;
using FoodApp.Services.ProductAPI.Models;
using FoodApp.Services.ProductAPI.Models.Dto;

namespace FoodApp.Services.ProductAPI.Service
{
    public interface IProductService : IGenericServiceAsync<Product, ProductDto>
    { }
    public class ProductService : GenericServiceAsync<Product, ProductDto>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
