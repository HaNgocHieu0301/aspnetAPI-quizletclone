using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public interface IUserRepository
	{
		Task<ApplicationUser> GetUserByUserNameAsync(string username);
		Task<ApplicationUser> GetUserByEmailAsync(string email);
		Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
		Task<IList<string>> GetRolesAsync(ApplicationUser user);
	}
}
