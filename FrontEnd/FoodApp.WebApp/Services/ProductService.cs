using FoodApp.WebApp.Models;
using FoodApp.WebApp.Models.ProductDto;
using static FoodApp.WebApp.Utilities.Constants;

namespace FoodApp.WebApp.Services
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllAsync();
        Task<ResponseDto?> GetByIdAsync(long id);
        Task<ResponseDto?> AddAsync(ProductDto dto);
        Task<ResponseDto?> DeleteAsync(ProductDto dto);
        Task<ResponseDto?> UpdateAsync(ProductDto dto);
    }
    public class ProductService(IBaseService baseService) : IProductService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto?> AddAsync(ProductDto dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = ProductAPIBase + "/api/product",
                ContentType = ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> UpdateAsync(ProductDto dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = dto,
                Url = ProductAPIBase + "/api/product",
                ContentType = ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> DeleteAsync(ProductDto dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Data = dto,
                Url = ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseDto?> GetByIdAsync(long id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + $"/api/product/{id}"
            });
        }
    }
}
