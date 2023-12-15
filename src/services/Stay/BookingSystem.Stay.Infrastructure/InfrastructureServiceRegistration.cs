using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Stay.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<StayContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("StayConnectionString")));

        //services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.AddScoped<IStayRepository, StayRepository>();

        //services.Configure<EmailSettings>(x => configuration.GetSection("EmailSettings"));
        //services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
