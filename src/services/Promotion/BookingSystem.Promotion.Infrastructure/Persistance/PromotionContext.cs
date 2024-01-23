using BookingSystem.Promotion.Domain.Common;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookingSystem.Promotion.Infrastructure.Persistance;

public class PromotionContext(DbContextOptions<PromotionContext> options) : DbContext(options), IPromotionDbContext
{
    private IDbContextTransaction _transaction;

    public DbSet<PromotionEntity> Promotions { get; set; }

    public DbSet<StayPromotionEntity> StayPromotions { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = 0;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = 0;
                    break;
                case EntityState.Deleted:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
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
