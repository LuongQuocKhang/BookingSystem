using BookingSystem.Booking.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookingSystem.Booking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //x.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        //services.AddMemoryCache();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
