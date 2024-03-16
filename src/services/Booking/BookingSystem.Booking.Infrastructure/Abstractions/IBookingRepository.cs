using BookingSystem.Booking.Domain.Entities;

namespace BookingSystem.Booking.Infrastructure.Abstractions;

public interface IBookingRepository
{
    public Task<int> BookStay(BookingEntity model, CancellationToken cancellationToken = default);
}
