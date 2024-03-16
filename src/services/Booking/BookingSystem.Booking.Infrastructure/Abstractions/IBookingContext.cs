using BookingSystem.Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Booking.Infrastructure.Abstractions;

public interface IBookingContext
{
    public DbSet<BookingEntity> Bookings { get; set; }

    void BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
