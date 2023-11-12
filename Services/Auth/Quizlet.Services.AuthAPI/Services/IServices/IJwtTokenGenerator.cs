using BusinessObject.Models;

namespace Quizlet.Services.AuthAPI.Services.IServices
{
	public interface IJwtTokenGenerator
	{
		string GeneratorToken(ApplicationUser user, IEnumerable<string> roles);
	}
}
