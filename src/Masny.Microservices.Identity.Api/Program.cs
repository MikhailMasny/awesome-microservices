using HealthChecks.UI.Client;
using Masny.Microservices.Auth.Interfaces;
using Masny.Microservices.Auth.Options;
using Masny.Microservices.Auth.Services;
using Masny.Microservices.Identity.Api.Data;
using Masny.Microservices.Identity.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

// fluentvalidation; optional: identityserver4, errorhandler, controller results
// redis, automapper, N-Layer or Clean arch



var builder = WebApplication.CreateBuilder(args);
var databaseSqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Logging
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Health check
builder.Services.AddHealthChecks()
    .AddSqlServer(databaseSqlConnection);

// Microsoft services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

// Custom services
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(databaseSqlConnection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddEventBusService(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
}

//INSERT INTO AspNetRoles VALUES(NEWID(), 'User', 'USER', NEWID());
//INSERT INTO AspNetRoles VALUES(NEWID(), 'Admin', 'ADMIN', NEWID());

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
