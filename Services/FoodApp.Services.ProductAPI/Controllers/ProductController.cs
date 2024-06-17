using FoodApp.Services.ProductAPI.Models.Dto;
using FoodApp.Services.ProductAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        protected ResponseDto _responseDto = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                _responseDto.Result = await _productService.GetAllAsync();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                _responseDto.Result = await _productService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
        {
            try
            {
                productDto = UpdateImageUrl(productDto);
                await _productService.AddAsync(productDto);
                _responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
        {
            try
            {
                productDto = UpdateImageUrl(productDto);
                await _productService.UpdateAsync(productDto);
                _responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> Delete([FromBody] ProductDto productDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(productDto.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), productDto.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                await _productService.DeleteAsync(productDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        private ProductDto UpdateImageUrl(ProductDto productDto)
        {
            if (productDto.ImageUrl != null)
            {
                string fileName = productDto.ProductId + Path.GetExtension(productDto.Image.FileName);
                string filePath = @"wwwroot\ProductImages\" + fileName;

                //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);
                if (file.Exists)
                {
                    file.Delete();
                }

                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    productDto.Image.CopyTo(fileStream);
                }
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                productDto.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                productDto.ImageLocalPath = filePath;
            }
            else
            {
                productDto.ImageUrl = "https://placehold.co/600x400";
            }
            return productDto;
        }
    }
}
