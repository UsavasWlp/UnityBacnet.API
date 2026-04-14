using UnityBacnet.API.Infrastructure.Auth;
using UnityBacnet.API.Infrastructure.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//Unity API Config
builder.Services.AddTransient<UnityAuthHandler>();

builder.Services.AddHttpClient<UnityAuthService>(client =>
{
    client.BaseAddress = new Uri("https://unity-api-url/");
}).AddHttpMessageHandler<UnityAuthHandler>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
