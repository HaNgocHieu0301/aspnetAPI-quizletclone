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
		ApplicationUser GetUserByUserName(string username);
		ApplicationUser GetUserByEmail(string email);
	}
}
