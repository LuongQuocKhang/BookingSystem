using BookingSystem.Cars.Api.Extensions;
using BookingSystem.Cars.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

using BookingSystem.Cars.Application;
using BookingSystem.Cars.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddConfigureSwaggerGen();

builder.Services.AddOpenApi(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ApiEndPoint:IdentityServerBaseAddress"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddServices();

builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseUrlRewrite();

IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseOpenApi(builder.Configuration, apiVersionDescriptionProvider);

app.AddGrapQL();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
