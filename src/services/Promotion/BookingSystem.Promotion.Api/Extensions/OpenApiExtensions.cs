using BookingSystem.Promotion.Api.Infrastructure.Swagger;
using BookingSystem.Promotion.Api.Rules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Promotion.Api.Extensions;

public static class OpenApiExtensions
{
    public static void UseUrlRewrite(this IApplicationBuilder app)
    {
        var options = new RewriteOptions();
        options.Rules.Add(new UrlRewriteRules());
        app.UseRewriter(options);
    }

    public static void AddOpenApi(this IServiceCollection services, IConfiguration config, Action<SwaggerGenOptions> additionalSetup = null)
    {
        services.AddVersionedApiExplorer(c =>
        {
            c.GroupNameFormat = "'v'VV";
        });
        services.AddApiVersioning(c =>
        {
            c.AssumeDefaultVersionWhenUnspecified = true;
            c.DefaultApiVersion = new ApiVersion(1, 0);
            c.ReportApiVersions = true;
        });

        IApiVersionDescriptionProvider apiVersionDescriptionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

        services.AddSwaggerGen(c =>
        {
            foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(item.GroupName, CreateInfoForApiVersion(item));
            }
            c.DescribeAllParametersInCamelCase();

            OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "JWT Authorization header using the Bearer scheme.",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    jwtSecurityScheme,
                    Array.Empty<string>()
                }
            });

            c.OperationFilter<RemoveVersionFromParameter>();
            c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            c.OperationFilter<CustomOperationIdFilter>();

            c.DocInclusionPredicate((documentName, apiDescription) =>
            {
                var actionApiVersionModel = apiDescription.ActionDescriptor.GetApiVersionModel(
                    ApiVersionMapping.Explicit |
                    ApiVersionMapping.Implicit);

                if (actionApiVersionModel == null)
                {
                    return true;
                }
                if (actionApiVersionModel.DeclaredApiVersions.Any())
                {
                    return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == documentName);
                }
                return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == documentName);
            });
            c.EnableAnnotations();

            additionalSetup?.Invoke(c);
        });
    }

    private static bool IsDevelopment()
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        environment ??= "production";
        bool isDevEnv = environment.Equals("DEVELOPMENT", StringComparison.InvariantCultureIgnoreCase);
        return isDevEnv;
    }

    public static void UseOpenApi(this IApplicationBuilder app, IConfiguration config, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        app.UseSwagger(options => { options.RouteTemplate = $"swagger/{{documentName}}/docs.json"; });
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
        });
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = $"Promotion API {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = "Promotion Management",
            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
            info.Description += " This API version has been deprecated.";

        return info;
    }
}  
