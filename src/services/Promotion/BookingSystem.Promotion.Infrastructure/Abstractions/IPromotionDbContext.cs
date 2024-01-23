using BookingSystem.Promotion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Promotion.Infrastructure.Abstractions;

public interface IPromotionDbContext
{
    public DbSet<PromotionEntity> Promotions { get; set; }

    public DbSet<StayPromotionEntity> StayPromotions { get; set; }

    void BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
