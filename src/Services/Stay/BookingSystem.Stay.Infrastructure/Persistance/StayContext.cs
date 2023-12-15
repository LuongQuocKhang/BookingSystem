using BookingSystem.Stay.Domain.Common;
using BookingSystem.Stay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Stay.Infrastructure.Persistance;

public class StayContext : DbContext
{
    public StayContext(DbContextOptions<StayContext> options) : base(options)
    { }

    public DbSet<Stays> Stays { get; set; }

    public DbSet<StayReview> StayReviews { get; set; }

    public DbSet<StayWishList> StayWishLists { get; set; }

    public DbSet<StayShare> StayShares { get; set; }

    public DbSet<Amenity> Amenities { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "admin";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "admin";
                    break;
                case EntityState.Deleted:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "admin";
                    break;
            }

        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
