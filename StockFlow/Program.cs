using System.Reflection;
using StockFlow.Api;
using StockFlow.Application;
using StockFlow.Domain;
using StockFlow.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApi()
    .AddInfraestructure(builder.Configuration)
    .AddApplication()
    .AddDomain();
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwagger();
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "StockFlow API";
    options.SwaggerDocumentUrlsPath = "openapi/v1.json";
});

app.MapControllers();

app.Run();
