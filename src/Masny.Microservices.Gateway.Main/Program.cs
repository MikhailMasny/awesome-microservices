using Masny.Microservices.Auth.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

// Microsoft services
builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = JwtOptions.GetSymmetricSecurityKey(),
            ValidateAudience = true,
            ValidAudience = JwtOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = JwtOptions.Issuer,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

// Configurations
builder.Configuration.AddJsonFile("ocelot.json");

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true);
}

// Custom services
builder.Services.AddOcelot();

// https://medium.com/@niteshsinghal85/3-ways-to-do-authorization-in-ocelot-api-gateway-in-asp-net-core-7ef8301b2f65

var app = builder.Build();

// dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p { password here }
// dotnet dev-certs https --trust

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSerilogRequestLogging();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseOcelot().Wait();

app.Run();
