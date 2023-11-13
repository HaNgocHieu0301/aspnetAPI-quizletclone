namespace BusinessObject.Models;

public class ResponseFogetPasswordWithToken
{
    public string email { get; set; }
    public string token { get; set; }
    public string resetUrl { get; set; }
}