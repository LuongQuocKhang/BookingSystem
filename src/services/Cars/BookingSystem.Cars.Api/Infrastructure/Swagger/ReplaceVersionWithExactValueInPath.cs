using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Cars.Api.Infrastructure.Swagger
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
                throw new ArgumentNullException(nameof(swaggerDoc));

            var paths = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
            {
                paths.Add(key.Replace("{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
