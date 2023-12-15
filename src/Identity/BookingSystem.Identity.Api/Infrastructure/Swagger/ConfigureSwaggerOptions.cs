using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Identity.Api.Swagger;

/// <summary>
///   Configures the Swagger generation options.
/// </summary>
/// <remarks>
///   This allows API versioning to define a Swagger document per API version after the <see
///   cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.
/// </remarks>
/// <remarks>
///   Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
/// </remarks>
/// <param name="provider">
///   The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.
/// </param>
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;

    /// <inheritdoc/>
    public void Configure(SwaggerGenOptions options)
    {
        // add a swagger document for each discovered API version
        // note: you might choose to skip or document deprecated API versions differently
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = "Bearer"
            //            }
            //        },
            //        Array.Empty<string>()
            //    }
            //});
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = $"IDENITY API {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = "IDENITY Management",
            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
            info.Description += " This API version has been deprecated.";

        return info;
    }
}