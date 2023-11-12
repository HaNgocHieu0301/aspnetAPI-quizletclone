using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class UserDAO
	{
		public static ApplicationUser GetUserByUserName(string userName)
		{
			using var _db = new AppDbContext();
			try
			{
				return _db.Users.AsNoTracking().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static ApplicationUser GetUserByEmail(string email)
		{
			using var _db = new AppDbContext();
			try
			{
				return _db.Users.AsNoTracking().FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
