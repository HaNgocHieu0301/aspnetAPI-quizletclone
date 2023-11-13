using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Quizlet.Services.AuthAPI.Services.IServices;
using Repositories;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc.Routing;
using Quizlet.Services.AuthAPI.RabbitMQSender;

namespace Quizlet.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRabbitMQAuthMessageSender _rabbitMQAuthMessageSender;

        public AuthService(IUserRepository userRepository, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator,
            IRabbitMQAuthMessageSender rabbitMqAuthMessageSender)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _rabbitMQAuthMessageSender = rabbitMqAuthMessageSender;
            _userManager = userManager;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            //var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            ApplicationUser user = await _userRepository.GetUserByEmailAsync(loginRequestDTO.Email);
            //var test = _userManager.FindByNameAsync(loginRequestDTO.UserName);
            bool isValid = await _userRepository.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDTO() { User = null, Token = "" };
            }

            var roles = await _userRepository.GetRolesAsync(user);
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
            var check = await _userManager.FindByEmailAsync(registrationRequestDTO.Email);
            if (check != null)
            {
                return "This email is already been used";
            }

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
                    var userToReturn = _userRepository.GetUserByEmailAsync(user.Email);
                    //UserDTO userDTO = new()
                    //{
                    //	Email = userToReturn.Email,
                    //	Id = userToReturn.Id,
                    //	UserName = userToReturn.UserName,
                    //	Streak = 1,
                    //	UsingTimeDay = 0,
                    //};
                    _rabbitMQAuthMessageSender.SendMessage(user.Email, "registeruser");
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

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
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

        public async Task<bool> RequestPasswordReset(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var frontendResetPasswordUrl = "http://localhost:/reset-password";
                var resetUrl =
                    $"{frontendResetPasswordUrl}?token={HttpUtility.UrlEncode(token)}&email={HttpUtility.UrlEncode(email)}";
                ResponseFogetPasswordWithToken responseFogetPasswordWithToken = new()
                {
                    email = email,
                    token = token,
                    resetUrl = resetUrl
                };
                _rabbitMQAuthMessageSender.SendMessage(responseFogetPasswordWithToken, "resetpassword");
                return true;
            }

            return false;
        }

        public async Task<bool> ResetPassword(RequestChangePasswordWithToken request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng
                return false;
            }
            var result = await _userManager.ResetPasswordAsync(user, request.token, request.newPassword);
            if (result.Succeeded)
            {
                // Mật khẩu đã được đặt lại thành công
                return true;
            }
            // Xử lý khi token không hợp lệ hoặc có lỗi
            return false;
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            var check = await _userManager.FindByEmailAsync(email);
            if (check != null)
            {
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDTO> LoginGoogle(string token)
        {
            try
            {
                using var client = new HttpClient();
                // Thiết lập header Authorization với Bearer token
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Thực hiện GET request
                var response = await client.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");

                if (response.IsSuccessStatusCode)
                {
                    // Đọc và trả về dữ liệu từ response
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var jsonDocument = JsonDocument.Parse(content);
                    string email = jsonDocument.RootElement.GetProperty("email").GetString();
                    var user = await _userRepository.GetUserByEmailAsync(email);
                    if (user != null)
                    {
                        var roles = await _userRepository.GetRolesAsync(user);
                        var jwt = _jwtTokenGenerator.GeneratorToken(user, roles);

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
                            Token = jwt
                        };

                        return loginResponseDTO;
                    }
                    else
                    {
                        UserDTO userDto = new()
                        {
                            Email = email,
                            Id = null,
                            UserName = null,
                            Streak = 1,
                            UsingTimeDay = 0,
                        };

                        LoginResponseDTO loginResponseDTO = new()
                        {
                            User = userDto,
                            Token = null
                        };
                        return loginResponseDTO;
                    }
                }
                else
                {
                    // Xử lý nếu có lỗi
                    // Có thể throw một exception hoặc xử lý theo cách khác tùy thuộc vào yêu cầu của bạn
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoginResponseDTO> LoginFacebook(string token)
        {
            using var client = new HttpClient();
            var response =
                await client.GetAsync(
                    "https://graph.facebook.com/me?fields=id,name,email,first_name,last_name,gender,picture&access_token=" +
                    token);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    // Đọc và trả về dữ liệu từ response
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var jsonDocument = JsonDocument.Parse(content);
                    string email = jsonDocument.RootElement.GetProperty("email").GetString();
                    var user = await _userRepository.GetUserByEmailAsync(email);
                    if (user != null)
                    {
                        var roles = await _userRepository.GetRolesAsync(user);
                        var jwt = _jwtTokenGenerator.GeneratorToken(user, roles);

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
                            Token = jwt
                        };

                        return loginResponseDTO;
                    }
                    else
                    {
                        UserDTO userDto = new()
                        {
                            Email = email,
                            Id = null,
                            UserName = null,
                            Streak = 1,
                            UsingTimeDay = 0,
                        };

                        LoginResponseDTO loginResponseDTO = new()
                        {
                            User = userDto,
                            Token = null
                        };
                        return loginResponseDTO;
                    }
                }
                else
                {
                    // Xử lý nếu có lỗi
                    // Có thể throw một exception hoặc xử lý theo cách khác tùy thuộc vào yêu cầu của bạn
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}