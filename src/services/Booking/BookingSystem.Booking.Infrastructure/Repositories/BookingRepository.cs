using BookingSystem.Booking.Domain.Entities;
using BookingSystem.Booking.Infrastructure.Abstractions;
using BookingSystem.Booking.Infrastructure.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Booking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IBookingContext _context;

    private readonly IPublishEndpoint _publishEndpoint;

    private readonly ILogger<BookingRepository> _logger;

    public BookingRepository(IBookingContext context, ILogger<BookingRepository> logger, IPublishEndpoint publishEndpoint)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<int> BookStay(BookingEntity model, CancellationToken cancellationToken = default)
    {
        try
        {
            model.StatusId = Domain.Constant.BookingStatus.INIT;

            _context.Bookings.Add(model);

            int result = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await _publishEndpoint.Publish<BookingAddedMessage>(new BookingAddedMessage()
                {
                    StayId = model.StayId,
                    UserId = model.UserId,
                    BookingId = model.Id
            }, cancellationToken).ConfigureAwait(false);

            return model.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when save booking: {message}", e.Message);
        }
        return default;
    }
}
