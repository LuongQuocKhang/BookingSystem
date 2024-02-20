using System.Net;
using BookingSystem.Booking.Application.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookingSystem.Booking.Api.Middlewares;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}

public class CustomExceptionHandlerMiddleware
{
    private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        },
        Formatting = Formatting.Indented
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (AccessDeniedException ex)
        {
            await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.Forbidden).ConfigureAwait(false);
        }
        catch (BusinessRuleException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var error = new Error
            {
                Code = ex.ErrorCode,
                Message = ex.Message,
                Details = ex.ValidationFailures.Select(x => new Error
                {
                    Code = x.ErrorCode,
                    Message = x.ErrorMessage,
                    Target = x.PropertyName,
                }),
            };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error, _jsonSerializerSettings)).ConfigureAwait(false);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.NotFound).ConfigureAwait(false);
        }
        catch (IntegrationException ex)
        {
            await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.InternalServerError).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.InternalServerError).ConfigureAwait(false);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        _logger.LogError(exception, exception.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var error = new Error { Message = exception.Message };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(error, _jsonSerializerSettings)).ConfigureAwait(false);
    }
}
