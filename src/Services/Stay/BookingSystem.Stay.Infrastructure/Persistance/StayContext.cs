using BookingSystem.Stay.Domain.Common;
using BookingSystem.Stay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Stay.Infrastructure.Persistance;

public class StayContext(DbContextOptions<StayContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.StayEntity> Stays { get; set; }

    public DbSet<StayReviewEntity> StayReviews { get; set; }

    public DbSet<StayWishListEntity> StayWishLists { get; set; }

    public DbSet<StayShareEntity> StayShares { get; set; }

    public DbSet<AmenityEntity> Amenities { get; set; }

    public DbSet<StayTagEntity> StayTags { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
}
