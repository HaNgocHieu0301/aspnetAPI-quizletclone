using BusinessObject.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Quizlet.Services.EmailAPI.Settings;

namespace Quizlet.Services.EmailAPI.Services;

public class EmailService : IEmailService
{
    private readonly MailSettings _settings;
    private readonly SmtpClient _smtpClient;
    public EmailService(MailSettings mailSettings)
    {
        _settings = mailSettings;
        
    }
    public async Task RegisterUserEmailAndSend(string? email,CancellationToken cancellationToken = new CancellationToken())
    {
        var emailRequest = new MailRequest
        {
            ToAddress = "hieuhn0301@gmail.com",
            Body = "Register successfully",
            Subject = "Register successfully"
        };
        try
        {
            await SendEmailAsync(emailRequest, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var emailMessage = new MimeMessage
        {
            Sender = new MailboxAddress(_settings.DisplayName, request.From ?? _settings.From),
            Subject = request.Subject,
            Body = new BodyBuilder
            {
                HtmlBody = request.Body
            }.ToMessageBody()
        };

        if (request.ToAddresses.Any())
        {
            foreach (var toAddress in request.ToAddresses)
            {
                emailMessage.To.Add(MailboxAddress.Parse(toAddress));
            }
        }
        else
        {
            var toAddress = request.ToAddress;
            emailMessage.To.Add(MailboxAddress.Parse(toAddress));
        }

        try
        {
            using var _smtpClient = new SmtpClient();
            await _smtpClient.ConnectAsync(_settings.Server, _settings.Port, _settings.UseSsl, cancellationToken);
            await _smtpClient.AuthenticateAsync(_settings.UserName, _settings.Password, cancellationToken);
            await _smtpClient.SendAsync(emailMessage, cancellationToken);
            await _smtpClient.DisconnectAsync(true, cancellationToken);
            _smtpClient.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task ForgotPassword(ResponseFogetPasswordWithToken responseFogetPasswordWithToken,CancellationToken cancellationToken = new CancellationToken())
    {
        var emailRequest = new MailRequest
        {
            ToAddress = responseFogetPasswordWithToken.email,
            Body = responseFogetPasswordWithToken.resetUrl,
            Subject = "Reset password"
        };
        try
        {
            await SendEmailAsync(emailRequest, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}