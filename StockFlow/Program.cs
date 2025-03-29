using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using StockFlow.Api;
using StockFlow.Application;
using StockFlow.Application.Features.Customer.Queries.GetAllCustomers;
using StockFlow.Domain;
using StockFlow.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApi()
    .AddInfraestructure(builder.Configuration)
    .AddApplication()
    .AddDomain();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQueryHandler).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StockFlow API",
        Version = "v1",
        Description = "API for managing inventory, customers, and sales.",
    });
});

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

app.MapGet("/getAllCustomer", async ([FromServices] IMediator mediator) =>
{
    return await mediator.Send(new GetAllCustomersQuery());
}).WithName("GetAllCustomers");


app.Run();
