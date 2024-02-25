using BookingSystem.Stay.Api.Extensions;
using BookingSystem.Stay.Api.Middlewares;
using BookingSystem.Stay.Application;
using BookingSystem.Stay.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("BookingSystem") ?? "");

builder.Services.AddHealthChecksUI()
            .AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseUrlRewrite();

IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseOpenApi(builder.Configuration, apiVersionDescriptionProvider);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseRouting().UseEndpoints(config =>
{
    config.MapHealthChecksUI();
});

// You could customize the endpoint
app.UseHealthChecksPrometheusExporter("/my-health-metrics");

// Customize HTTP status code returned(prometheus will not read health metrics when a default HTTP 503 is returned)
app.UseHealthChecksPrometheusExporter("/my-health-metrics", options => options.ResultStatusCodes[HealthStatus.Unhealthy] = (int)HttpStatusCode.OK);

app.Run();
