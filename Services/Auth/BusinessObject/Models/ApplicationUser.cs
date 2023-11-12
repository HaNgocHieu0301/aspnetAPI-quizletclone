using Microsoft.AspNetCore.Identity;

namespace BusinessObject.Models
{
	public class ApplicationUser : IdentityUser
	{
		public int Streak { get; set; }
		public float UsingTimeDay { get; set; }
	}
}
