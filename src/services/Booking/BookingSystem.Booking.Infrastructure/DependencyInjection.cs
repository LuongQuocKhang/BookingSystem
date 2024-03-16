using BookingSystem.Booking.Infrastructure.Abstractions;
using BookingSystem.Booking.Infrastructure.GrpcServices;
using BookingSystem.Booking.Infrastructure.Persistance;
using BookingSystem.Booking.Infrastructure.Repositories;
using BookingSystem.Stay.gRPC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Booking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BookingSystem")));

        services.AddGrpcClient<StayService.StayServiceClient>(config =>
        {
            config.Address = new Uri(configuration["ApiEndPoint:StayGrpcService"] ?? "");
        });

        services.AddTransient<IStayGrpcService, StayGrpcService>();

        services.AddTransient<IBookingRepository, BookingRepository>();

        services.AddTransient<IBookingContext, BookingContext>();

        return services;
    }

}
