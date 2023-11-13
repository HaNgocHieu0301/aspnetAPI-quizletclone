namespace BusinessObject.Models;

public class RequestChangePasswordDTO
{
    public string oldPassword { get; set; }
    public string newPassword { get; set; }
    public string confirmNewPassword { get; set; }
    public string email { get; set; }
}