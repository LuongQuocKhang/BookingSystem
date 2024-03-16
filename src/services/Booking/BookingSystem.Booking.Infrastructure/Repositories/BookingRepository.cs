using BookingSystem.Booking.Domain.Entities;
using BookingSystem.Booking.Infrastructure.Abstractions;

namespace BookingSystem.Booking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IBookingContext _context;

    public BookingRepository(IBookingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> BookStay(BookingEntity model, CancellationToken cancellationToken = default)
    {
        _context.Bookings.Add(model);

        int result = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return result;
    }
}
