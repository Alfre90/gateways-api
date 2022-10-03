
using Gateways.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;

// Add services to the container.

services.AddControllers();

// Configure automapper with all automapper profiles
services.AddAutoMapper(typeof(GatewaysServicesConfiguration));

// Register Services
services.AddGatewaysServicesConfiguration();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateways API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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

