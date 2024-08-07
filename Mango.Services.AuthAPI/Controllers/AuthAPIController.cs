using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registerDto)
        {
            var errorMessage = await _authService.Register(registerDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;

                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var loginResponse = await _authService.Login(loginDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";
                return BadRequest(_responseDto);
            }

            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(RegistrationRequestDto registrationRequest)
        {
            var isAssignSucceful = await _authService.AssignRole(registrationRequest.Email, registrationRequest.Role.ToUpper());
            if (!isAssignSucceful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
