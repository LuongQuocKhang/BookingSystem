using BookingSystem.Booking.Domain.Common;
using BookingSystem.Booking.Domain.Entities;
using BookingSystem.Booking.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookingSystem.Booking.Infrastructure.Persistance;

public class BookingContext(DbContextOptions<BookingContext> options) : DbContext(options), IBookingContext
{
    private IDbContextTransaction _transaction;

    public DbSet<BookingEntity> Bookings { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = 0;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = 0;
                    break;
                case EntityState.Deleted:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = 0;
                    break;
            }

        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transaction?.Commit();
        _transaction?.Dispose();
        _transaction = null;
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }
}
