using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public UserRepository(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<ApplicationUser> GetUserByUserNameAsync(string username) => await _userManager.FindByNameAsync(username);
		public async Task<ApplicationUser> GetUserByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);
		public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => await _userManager.CheckPasswordAsync(user, password);
		public async Task<IList<string>> GetRolesAsync(ApplicationUser user) => await _userManager.GetRolesAsync(user);
	}
}
