using BookingSystem.Notification.Infrastructure.Comsumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Notification.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        MessageBrokerOptions? options = configuration.GetSection(MessageBrokerOptions.BindLocator).Get<MessageBrokerOptions>()
                ?? throw new InvalidOperationException("Rabbitmq configuration not found.");

        if (string.IsNullOrEmpty(options?.Notification?.Host))
        {
            throw new InvalidOperationException("Rabbitmq ConnectionString must not empty.");
        }

        services.AddMassTransit(x =>
        {
            x.AddConsumer<BookingAddedConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(options.Notification.Host, options.Notification.VirtualHost, config =>
                {
                    config.Username(options.Notification.User);
                    config.Password(options.Notification.Password);
                });

                cfg.ReceiveEndpoint("booking-added", ep =>
                {
                    ep.ConfigureConsumer<BookingAddedConsumer>(ctx);
                });

            });
        });

        return services;
    }
}
