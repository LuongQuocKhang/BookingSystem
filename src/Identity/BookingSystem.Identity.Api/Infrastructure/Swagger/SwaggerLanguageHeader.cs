using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Identity.Api.Infrastructure.Swagger;

public class SwaggerLanguageHeader : IOperationFilter
{
    private readonly IServiceProvider _serviceProvider;

    public SwaggerLanguageHeader(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();
    }
}