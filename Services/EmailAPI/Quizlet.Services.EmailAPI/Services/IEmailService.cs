using BusinessObject.Models;

namespace Quizlet.Services.EmailAPI.Services;

public interface IEmailService
{
    Task RegisterUserEmailAndSend(string? email,CancellationToken cancellationToken = new CancellationToken());
    Task SendEmailAsync(MailRequest mailRequest, CancellationToken cancellationToken = new CancellationToken());
}