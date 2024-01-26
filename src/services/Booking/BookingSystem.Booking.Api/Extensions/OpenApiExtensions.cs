using BookingSystem.Booking.Api.Infrastructure.Swagger;
using BookingSystem.Booking.Api.Rules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Booking.Api.Extensions
{
    /// <summary>
    /// Extension methods to configurate OpenApi, Swagger
    /// </summary>
    public static class OpenApiExtensions
    {
        /// <summary>
        /// Rewrite Url Rule
        /// </summary>
        public static void UseUrlRewrite(this IApplicationBuilder app)
        {
            var options = new RewriteOptions();
            options.Rules.Add(new UrlRewriteRules());
            app.UseRewriter(options);
        }

        /// <summary>
        /// Add OpenApi, versioning, swagger
        /// </summary>
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

        /// <summary>
        /// Use OpenApi
        /// </summary>
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
                Title = $"STAYING API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Stay Management",
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
                info.Description += " This API version has been deprecated.";

            return info;
        }
    }  
}
