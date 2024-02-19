using BookingSystem.Promotion.gRPC.Protos;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Infrastructure.Abstractions;
using BookingSystem.Stay.Infrastructure.GrpcServices;
using BookingSystem.Stay.Infrastructure.Persistance;
using BookingSystem.Stay.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Stay.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StayContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BookingSystem")));

        services.AddGrpcClient<PromotionService.PromotionServiceClient>(config =>
        {
            config.Address = new Uri(configuration["ApiEndPoint:PromotionGrpcService"] ?? "");
        });

        services.AddScoped<PromotionGrpcService>();

        services.AddTransient<IStayDbContext, StayContext>();

        services.AddTransient<IStayRepository, StayRepository>();
        services.AddTransient<IAmenityRepository, AmenityRepository>();

        return services;
    }
}
