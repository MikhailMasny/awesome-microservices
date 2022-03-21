using HealthChecks.UI.Client;
using Masny.Microservices.Auth.Constants;
using Masny.Microservices.Auth.Options;
using Masny.Microservices.Profile.Api.Data;
using Masny.Microservices.Profile.Api.Extensions;
using Masny.Microservices.Profile.Api.Interfaces;
using Masny.Microservices.Profile.Api.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

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
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = SwaggerConstants.Security.Description,
        Name = SwaggerConstants.Security.Name,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = SwaggerConstants.Security.HttpAuth,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = SwaggerConstants.Security.Schema
        }
    };
    config.AddSecurityDefinition(SwaggerConstants.Security.Schema, securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securitySchema, new[] { SwaggerConstants.Security.Schema }
        }
    };
    config.AddSecurityRequirement(securityRequirement);
});

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

// Custom contexts
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(databaseSqlConnection));

// Custom managers & services
builder.Services.AddScoped<IProfileManager, ProfileManager>();
builder.Services.AddEventBusService(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
}

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
