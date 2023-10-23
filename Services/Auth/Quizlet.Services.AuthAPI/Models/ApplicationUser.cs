using Microsoft.AspNetCore.Identity;

namespace Quizlet.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public int Streak { get; set; }
		public float UsingTimeDay { get; set; }
	}
}
