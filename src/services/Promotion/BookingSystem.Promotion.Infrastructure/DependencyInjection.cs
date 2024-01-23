using BookingSystem.Promotion.Infrastructure.Abstractions;
using BookingSystem.Promotion.Infrastructure.Persistance;
using BookingSystem.Promotion.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Promotion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PromotionContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("BookingSystem")));

        services.AddTransient<IPromotionRepository, PromotionRepository>();
        return services;
    }
}
