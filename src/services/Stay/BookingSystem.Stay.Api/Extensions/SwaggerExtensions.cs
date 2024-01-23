using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BookingSystem.Stay.Api.Extensions
{
    /// <summary>
    /// Extension methods to configurate Swagger gen, 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Add swagger gen configuartion
        /// </summary>
        public static IServiceCollection AddConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{executingAssembly.GetName().Name}.xml"));

                // Additionally include the documentation of all other "relevant" projects
                IEnumerable<string> referencedProjectsXmlDocPaths = executingAssembly.GetReferencedAssemblies()
                    .Where(assembly => assembly.Name != null && assembly.Name.StartsWith("Matching", StringComparison.InvariantCultureIgnoreCase))
                    .Select(assembly => Path.Combine(AppContext.BaseDirectory, $"{assembly.Name}.xml"))
                    .Where(path => File.Exists(path));
                foreach (string xmlDocPath in referencedProjectsXmlDocPaths)
                {
                    options.IncludeXmlComments(xmlDocPath);
                }
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        //public static IServiceCollection AddConfigureObservability(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddApplicationInsightsTelemetryProcessor<SuccessfulDependencyFilter>();
        //    services.AddObservability(new ObservabilityOptions
        //    {
        //        ApplicationInsightsServiceOptions = new ApplicationInsightsServiceOptions()
        //        {
        //            EnableEventCounterCollectionModule = false,
        //            ConnectionString = configuration["ApplicationInsights:ConnectionString"]
        //        },
        //        ApplicationName = configuration["ApplicationInsights:ApplicationName"],
        //        EnableShortDependencyTelemetryFilter = true
        //    });
        //    return services;
        //}

        /// <summary>
        /// Add cors configuartion
        /// </summary>
        public static WebApplicationBuilder AddConfigureCors(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddCors(options =>
            {
                string[] allowedUrls = configuration.GetAllowedUrls("AllowedUrls");

                if (!builder.Environment.IsProduction())
                {
                    string[] localAllowedUrls = configuration.GetAllowedUrls("LocalFeUrls");
                    allowedUrls = allowedUrls.Concat(localAllowedUrls).ToArray();
                }

                options.AddPolicy("AllowErpDefault", builder => builder
                    .WithOrigins(allowedUrls)
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return builder;
        }

        /// <summary>
        /// Add allowed URLs
        /// </summary>
        public static string[] GetAllowedUrls(this IConfiguration configuration, string urlConfiguration)
        {
            string? configuredAllowedUrls = configuration[urlConfiguration];
            List<string> allowedUrls = !string.IsNullOrEmpty(configuredAllowedUrls)
                ? configuredAllowedUrls.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                : new List<string>();
            return allowedUrls.ToArray();
        }
    }
}
