using BookingSystem.ApiGateways.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ApiEndPoint:IdentityServerBaseAddress"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,

        };
    });

builder.Services.AddOcelot()
    //.AddCacheManager(settings => settings.WithDictionaryHandle())
    .AddDelegatingHandler<JWTAuthenticationHandler>();

var app = builder.Build();

await app.UseOcelot();
app.UseAuthentication();

app.UseHttpsRedirection();

app.Run();
