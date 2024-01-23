using BookingSystem.Stay.Domain.Common;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace BookingSystem.Stay.Infrastructure.Persistance;

public class StayContext(DbContextOptions<StayContext> options) : DbContext(options), IStayDbContext
{
    private IDbContextTransaction _transaction;

    public DbSet<StayEntity> Stays { get; set; }

    public DbSet<RoomRateEntity> RoomRates { get; set; }

    public DbSet<StayReviewEntity> StayReviews { get; set; }

    public DbSet<StayWishListEntity> StayWishLists { get; set; }

    public DbSet<StayShareEntity> StayShares { get; set; }

    public DbSet<StayImageEntity> StayImages { get; set; }

    public DbSet<StayAmenityEntity> StayAmenities { get; set; }

    public DbSet<AmenityEntity> Amenities { get; set; }

    public DbSet<StayUnAvailabilityEntity> StayUnAvailability { get; set; }

    public DbSet<StayTagEntity> StayTags { get; set; }

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
