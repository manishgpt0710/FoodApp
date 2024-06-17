using FoodApp.Services.AuthAPI.Models.Dto;
using FoodApp.Services.AuthAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace FoodApp.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        protected ResponseDto _responseDto = new();

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequest)
        {
            var errorMessage = await _authService.Register(registrationRequest);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message  = errorMessage;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginRespone = await _authService.Login(model);
            if (loginRespone.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Usernamme or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginRespone;
            return Ok(_responseDto);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleResponse = await _authService.AssignRole(model.Email, model.Role.GetDisplayName());
            if (!assignRoleResponse)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Unable to assign role";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = assignRoleResponse;
            return Ok(_responseDto);
        }
    }
}
