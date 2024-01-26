using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Booking.Api.Infrastructure.Swagger
{
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "api-version" || p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
