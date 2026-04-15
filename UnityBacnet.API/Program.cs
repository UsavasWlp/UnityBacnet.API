using Polly;
using Polly.Extensions.Http;
using UnityBacnet.API.Infrastructure.Auth;
using UnityBacnet.API.Infrastructure.Http;
using UnityBacnet.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//Polly Policy for Retry / Timeout / Circuit Breaker
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt =>
            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        );
}

//Unity API Config
builder.Services.Configure<UnityApiSettings>(
    builder.Configuration.GetSection("UnityApi")
);
builder.Services.AddTransient<UnityAuthHandler>();
builder.Services.AddHttpClient<UnityAuthService>(client =>
{
    client.BaseAddress = new Uri("https://unity-api-url/");
}).AddHttpMessageHandler<UnityAuthHandler>()
.AddPolicyHandler(GetRetryPolicy());

//Global Validation
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
