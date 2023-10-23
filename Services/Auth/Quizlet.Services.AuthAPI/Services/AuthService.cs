using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Quizlet.Services.AuthAPI.Services.IServices;
using Repositories;
using System;

namespace Quizlet.Services.AuthAPI.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		public AuthService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
		{
			_userRepository = userRepository;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}
		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			//var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
			ApplicationUser user = _userRepository.GetUserByUserName(loginRequestDTO.UserName);
			bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

			if (user == null || !isValid)
			{
				return new LoginResponseDTO() { User = null, Token = "" };
			}

			var roles = await _userManager.GetRolesAsync(user);
			var token = _jwtTokenGenerator.GeneratorToken(user, roles);

			UserDTO userDto = new()
			{
				Email = user.Email,
				Id = user.Id,
				UserName = user.UserName,
				Streak = 1,
				UsingTimeDay = 0,
			};

			LoginResponseDTO loginResponseDTO = new()
			{
				User = userDto,
				Token = token
			};

			return loginResponseDTO;
		}

		public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
		{
			ApplicationUser user = new()
			{
				UserName = registrationRequestDTO.Username,
				Email = registrationRequestDTO?.Email,
				NormalizedEmail = registrationRequestDTO?.Email.ToUpper(),
				Streak = 1,
				UsingTimeDay = 0,
			};

			try
			{
				var result = await _userManager.CreateAsync(user, registrationRequestDTO?.Password);
				if (result.Succeeded)
				{
					var assignRoleResult = await AssignRole(user, registrationRequestDTO.Role);
					var userToReturn = _userRepository.GetUserByEmail(user.Email);
					//UserDTO userDTO = new()
					//{
					//	Email = userToReturn.Email,
					//	Id = userToReturn.Id,
					//	UserName = userToReturn.UserName,
					//	Streak = 1,
					//	UsingTimeDay = 0,
					//};
					return "";
				}
				else
				{
					return result.Errors.FirstOrDefault().Description;
				}
			}
			catch (Exception ex)
			{

			}
			return "Error Encountered";
		}
		public async Task<bool> AssignRole(ApplicationUser user, string roleName)
		{
			//var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
			//var user = _userRepository.GetUserByEmail(email);
			if (user != null)
			{
				if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
				{
					_roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
				}
				await _userManager.AddToRoleAsync(user, roleName);
				return true;
			}
			return false;
		}
	}
}
