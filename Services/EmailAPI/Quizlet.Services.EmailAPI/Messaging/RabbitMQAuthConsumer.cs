using System.Text;
using BusinessObject.Models;
using Newtonsoft.Json;
using Quizlet.Services.EmailAPI.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Quizlet.Services.EmailAPI.Messaging;

public class RabbitMQAuthConsumer : BackgroundService
{
    private readonly EmailService _emailService;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQAuthConsumer(EmailService emailService)
    {
        _emailService = emailService;
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Password = "guest",
            UserName = "guest"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("registeruser", false, false, false, null);
        _channel.QueueDeclare("forgotpassword", false, false, false, null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var registerConsumer = new EventingBasicConsumer(_channel);
        registerConsumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            String email = JsonConvert.DeserializeObject<string>(content);
            HandleMessage(email,stoppingToken).GetAwaiter().GetResult();
            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("registeruser", false, registerConsumer);

        // Consumer cho hàng đợi quên mật khẩu
        var forgotPasswordConsumer = new EventingBasicConsumer(_channel);
        forgotPasswordConsumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            ResponseFogetPasswordWithToken obj = JsonConvert.DeserializeObject<ResponseFogetPasswordWithToken>(content);
            HandleForgotPassword(obj,stoppingToken).GetAwaiter().GetResult();
            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("forgotpassword", false, forgotPasswordConsumer);
        
        return Task.CompletedTask;
    }

    private async Task HandleForgotPassword(ResponseFogetPasswordWithToken? obj, CancellationToken token)
    {
        _emailService.ForgotPassword(obj, token).GetAwaiter().GetResult();
    }

    private async Task HandleMessage(string? email, CancellationToken token)
    {
        _emailService.RegisterUserEmailAndSend(email,token).GetAwaiter().GetResult();
    }
}