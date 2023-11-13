using BusinessObject.Services.Interfaces;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Quizlet.Services.FlashcardAPI.Services.Implementations;
using Repository;
using Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

builder.Services.AddScoped<IQuestionService, QuestionService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100));

var app = builder.Build();
app.UseCors(b =>
{
    b
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

app.Use((context, next) =>
{
    var endpoint = context.GetEndpoint();
    if (endpoint == null)
    {
        return next();
    }
    IEnumerable<string> templates;
    IODataRoutingMetadata metadata = endpoint.Metadata.GetMetadata<IODataRoutingMetadata>();
    if (metadata != null)
    {
        templates = metadata.Template.GetTemplates();
    }

    return next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
