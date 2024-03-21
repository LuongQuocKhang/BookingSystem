using BookingSystem.Booking.Infrastructure.Abstractions;
using BookingSystem.Booking.Infrastructure.Configurations;
using BookingSystem.Booking.Infrastructure.GrpcServices;
using BookingSystem.Booking.Infrastructure.Persistance;
using BookingSystem.Booking.Infrastructure.Repositories;
using BookingSystem.Stay.gRPC;
using MassTransit;
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

        services.AddPublishers(configuration);

        return services;
    }

    public static IServiceCollection AddPublishers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            MessageBrokerOptions? options = configuration.GetSection(MessageBrokerOptions.BindLocator).Get<MessageBrokerOptions>() 
                ?? throw new InvalidOperationException("Rabbitmq configuration not found.");

            if (string.IsNullOrEmpty(options?.Notification?.Host))
            {
                throw new InvalidOperationException("Rabbitmq ConnectionString must not empty.");
            }

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(options.Notification.Host, options.Notification.VirtualHost, config =>
                {
                    config.Username(options.Notification.User);
                    config.Password(options.Notification.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
