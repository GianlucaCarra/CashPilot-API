using CashPilot.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();