using BusinessObject.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder o = new();
    o.EntitySet<Lesson>("Lessons");
    o.EntitySet<Answer>("Answers");
    o.EntitySet<Question>("Questions");
    return o.GetEdmModel();
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));

var app = builder.Build();

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
