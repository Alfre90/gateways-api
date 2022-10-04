
using FluentValidation;
using Gateways.Database;
using Gateways.Services;
using Gateways.Services.Common.Sieve.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;

//FluentValidation config
ValidatorOptions.Global.LanguageManager.Enabled = false;

// Add services to the container.

// For Entity Framework
services.AddDbContext<GatewaysContext>();

services.AddSieve(builder.Configuration.GetSection("Sieve"));

services.AddControllers();

// Configure automapper with all automapper profiles
services.AddAutoMapper(typeof(GatewaysServicesConfiguration));

// Register Services
services.AddGatewaysServicesConfiguration();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateways API", Version = "v1" });
});

var app = builder.Build();

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseStaticFiles();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

