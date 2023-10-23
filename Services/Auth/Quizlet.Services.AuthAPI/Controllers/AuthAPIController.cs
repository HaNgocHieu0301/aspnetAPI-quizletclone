using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
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

		//[HttpPost("assignRole")]
		//public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model)
		//{
		//	var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
		//	if (!assignRoleSuccessful)
		//	{
		//		_responseDTO.IsSuccess = false;
		//		_responseDTO.Message = "Error encountered!";
		//		return BadRequest(_responseDTO);
		//	}
		//	return Ok(_responseDTO);
		//}
	}
}
