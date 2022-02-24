using Masny.Microservices.Identity.Api.Interfaces;
using Masny.Microservices.Identity.Api.Services;
using Masny.Microservices.Identity.Api.Settings;

// serilog, fluentvalidation; optional: identityserver4

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom services
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IJwtService, JwtService>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            // укзывает, будет ли валидироваться издатель при валидации токена
//            ValidateIssuer = true,
//            // строка, представляющая издателя
//            ValidIssuer = AuthOptions.ISSUER,

//            // будет ли валидироваться потребитель токена
//            ValidateAudience = true,
//            // установка потребителя токена
//            ValidAudience = AuthOptions.AUDIENCE,
//            // будет ли валидироваться время существования
//            ValidateLifetime = true,

//            // установка ключа безопасности
//            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//            // валидация ключа безопасности
//            ValidateIssuerSigningKey = true,
//        };
//    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
