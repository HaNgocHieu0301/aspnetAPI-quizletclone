using BusinessObject.Models;

namespace Quizlet.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LoginResponseDTO> LoginGoogle(string token);
        Task<LoginResponseDTO> LoginFacebook(string token);
        Task<bool> CheckEmailExist(string email);
        //Task<bool> AssignRole(string email, string roleName);
        Task<bool> AssignRole(ApplicationUser user, string roleName);
        Task<bool> AssignRole(string email, string roleName);
        Task<bool> RequestPasswordReset(string email);
        Task<bool> ResetPassword(RequestChangePasswordWithToken request);
    }
}
