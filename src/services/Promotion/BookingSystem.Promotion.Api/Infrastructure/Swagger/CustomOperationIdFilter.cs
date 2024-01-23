using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingSystem.Promotion.Api.Infrastructure.Swagger
{
    public class CustomOperationIdFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId == null)
                operation.OperationId = context.MethodInfo.Name;

            var namedArgument = context.MethodInfo.CustomAttributes
                .FirstOrDefault(x => x.GetType() == typeof(HttpMethodAttribute))
                ?.NamedArguments.FirstOrDefault(x => x.MemberName == "Name");

            var operationId = namedArgument?.TypedValue.ToString();

            if (operationId != null)
                operation.OperationId = operationId;
        }
    }
}
