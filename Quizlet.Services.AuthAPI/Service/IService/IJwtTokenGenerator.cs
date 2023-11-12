using Quizlet.Services.AuthAPI.Models;

namespace Quizlet.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GeneratorToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
