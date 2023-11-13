namespace Quizlet.Services.EmailAPI.Settings;

public class MailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string DisplayName { get; set; }
    public string From { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool UseSsl { get; set; }
}