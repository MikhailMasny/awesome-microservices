using Masny.Microservices.Profile.Api.Data;
using Masny.Microservices.Profile.Api.Extensions;
using Masny.Microservices.Profile.Api.Interfaces;
using Masny.Microservices.Profile.Api.Managers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom services
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEventBusService(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IProfileManager, ProfileManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
