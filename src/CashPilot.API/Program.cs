using CashPilot.Extensions;
using CashPilot.Infrastructure.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApplicationDependencies()
    .AddValidation()
    .AddMapper()
    .AddDatabase(configuration, environment)
    .AddAuth(configuration)
    .AddEmail(configuration)
    .AddRedisCache(configuration);
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;  
    options.LowercaseQueryStrings = true;
});

builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseForwardedHeaders();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();