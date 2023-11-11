﻿using BusinessObject.Models;

namespace Quizlet.Services.AuthAPI.Services.IServices
{
	public interface IAuthService
	{
		Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		//Task<bool> AssignRole(string email, string roleName);
		Task<bool> AssignRole(ApplicationUser user, string roleName);
		Task<bool> AssignRole(string email, string roleName);

	}
}