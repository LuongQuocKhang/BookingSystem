using BookingSystem.Notification.Infrastructure.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Notification.Infrastructure.Comsumers;

public class BookingAddedConsumer : IConsumer<BookingAddedMessage>
{
    private readonly ILogger<BookingAddedConsumer> _logger;

    public BookingAddedConsumer(ILogger<BookingAddedConsumer> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<BookingAddedMessage> context)
    {
        _logger.LogInformation(" [*] User {userId} book Stay {stayId}", context.Message.UserId, context.Message.StayId);
    }
}
