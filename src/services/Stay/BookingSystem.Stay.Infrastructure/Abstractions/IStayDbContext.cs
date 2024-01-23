using BookingSystem.Stay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingSystem.Stay.Infrastructure.Abstractions;

public interface IStayDbContext
{
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

    void BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
