namespace BusinessObject.Models;

public class RequestChangePasswordWithToken
{
    public string email { get; set; }
    public string token { get; set; }
    public string newPassword { get; set; }
}