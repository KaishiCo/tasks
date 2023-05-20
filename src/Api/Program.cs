using Api;
using Api.Endpoints;
using Application;
using Application.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication(builder.Configuration);

var app = builder.Build();
app.MapAuthEndpoints();
app.MapTaskItemsEndpoints();
app.MapGet("/", () => "Hello World!");

var dbInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await dbInitializer.InitializeAsync();
app.Run();
