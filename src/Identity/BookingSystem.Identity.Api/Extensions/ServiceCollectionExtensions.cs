using BookingSystem.Identity.Api.Infrastructure.Swagger;
using BookingSystem.Identity.Api.Swagger;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BookingSystem.Identity.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAndConfigApiVersioning(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                })
            .AddApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds
                    // IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                })
            // this enables binding ApiVersion as a endpoint callback parameter. if you don't use
            // it, then you should remove this configuration.
            .EnableApiVersionBinding();

        return services;
    }

    public static IServiceCollection AddAndConfigSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            // add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();
            options.OperationFilter<SwaggerLanguageHeader>();

            // JWT Bearer Authorization

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}