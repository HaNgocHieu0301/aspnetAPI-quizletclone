namespace Quizlet.Services.AuthAPI.Models.DTO
{
	public class UserDTO
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public int Streak { get; set; }
		public float UsingTimeDay { get; set; }
	}
}
