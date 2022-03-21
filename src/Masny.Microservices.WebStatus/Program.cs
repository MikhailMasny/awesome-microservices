// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(config => config.MapHealthChecksUI());

app.Run();
