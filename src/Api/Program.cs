using Api.Endpoints;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.MapAuthEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();
