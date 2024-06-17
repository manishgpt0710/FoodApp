using FoodApp.WebApp.Models;
using FoodApp.WebApp.Models.AuthModelDto;
using static FoodApp.WebApp.Utilities.Constants;

namespace FoodApp.WebApp.Services
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegistrationAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }

    public class AuthService(IBaseService baseService) : IAuthService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto() { 
                ApiType= ApiType.POST,
                Data = registrationRequestDto,
                Url = AuthAPIBase + "/api/auth/assignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = loginRequestDto,
                Url = AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDto?> RegistrationAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
