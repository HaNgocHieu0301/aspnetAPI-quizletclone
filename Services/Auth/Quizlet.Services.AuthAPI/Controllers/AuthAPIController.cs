using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Quizlet.Services.AuthAPI.Services.IServices;

namespace Quizlet.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDTO _responseDTO;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDTO = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            var errorMessage = await _authService.Register(registrationRequestDTO);
            //var assignRoleSuccessful = await _authService.AssignRole(registrationRequestDTO.Email, registrationRequestDTO.Role.ToUpper());
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = errorMessage;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponseDTO = await _authService.Login(loginRequestDTO);
            if (loginResponseDTO.User == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Login Failed!\n Username or password is incorrect! ";
                return BadRequest(_responseDTO);
            }
            _responseDTO.Result = loginResponseDTO;
            return Ok(_responseDTO);
        }

        [HttpGet("loginGoogle/{token}")]
        public async Task<IActionResult> LoginGoogle([FromRoute] string token)
        {
            var loginResponseDTO = await _authService.LoginGoogle(token);
            _responseDTO.Result = loginResponseDTO;
            return Ok(_responseDTO);
        }

        [HttpGet("loginFacebook/{token}")]
        public async Task<IActionResult> LoginFacebook([FromRoute] string token)
        {
            var loginResponseDTO = await _authService.LoginFacebook(token);
            _responseDTO.Result = loginResponseDTO;
            return Ok(_responseDTO);
        }

        [HttpGet("checkEmailExist/{email}")]
        public async Task<IActionResult> CheckEmailExist([FromRoute] string email)
        {
            var check = await _authService.CheckEmailExist(email);
            _responseDTO.Result = check;
            return Ok(_responseDTO);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Error encountered!";
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            //var user = 
            return Ok();
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string email)
        {
            //var user = 
            return Ok();
        }
        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> CheckResetToken([FromBody] string email)
        {
            //var user = 
            return Ok();
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] string email)
        {
            //var user = 
            return Ok();
        }
    }
}
