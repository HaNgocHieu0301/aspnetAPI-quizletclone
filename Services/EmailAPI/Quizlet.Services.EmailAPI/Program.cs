using Quizlet.Services.EmailAPI.Messaging;
using Quizlet.Services.EmailAPI.Services;
using Quizlet.Services.EmailAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddScoped<IEmailService, EmailService>();
var emailSetting = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();;
builder.Services.AddSingleton(new EmailService(emailSetting));
builder.Services.AddHostedService<RabbitMQAuthConsumer>();
// builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton(emailSetting);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();