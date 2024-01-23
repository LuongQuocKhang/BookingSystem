using BookingSystem.Promotion.Infrastructure;
using BookingSystem.Promotion.Application;
using BookingSystem.Promotion.Api.Extensions;
using BookingSystem.Promotion.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
