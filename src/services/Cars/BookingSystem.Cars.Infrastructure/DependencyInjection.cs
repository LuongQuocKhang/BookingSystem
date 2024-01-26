using BookingSystem.Cars.Domain.Queries;
using BookingSystem.Cars.Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Cars.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarsContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("BookingSystem")));

        services.AddGraphQLServer()
            .RegisterDbContext<CarsContext>()
            .InitializeOnStartup()
            .AddQueryType<Query>();

        return services;
    }
    
    public static WebApplication AddGrapQL(this WebApplication app)
    {
        app.MapGraphQL();

        return app;
    }
}
