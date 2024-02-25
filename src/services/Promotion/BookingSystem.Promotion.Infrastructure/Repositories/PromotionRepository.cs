using AutoMapper;
using BookingSystem.Promotion.Application.Abstractions;
using BookingSystem.Promotion.Application.Constant;
using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace BookingSystem.Promotion.Infrastructure.Repositories;

/// <summary>
/// Promotion Repository ( Get, Create, Delete, Update promotion )
/// </summary>
public class PromotionRepository(IPromotionDbContext context, IMapper mapper, ILogger<PromotionRepository> logger) : IPromotionRepository
{
    private readonly IPromotionDbContext _context = context;

    private readonly IMapper _mapper = mapper;

    private readonly ILogger<PromotionRepository> _logger = logger;

    /// <summary>
    /// Asynchronously create promotion
    /// </summary>
    /// <param name="model">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains created promotion id</returns>
    public async Task<int> CreatePromotion(PromotionEntity model, CancellationToken cancellationToken = default)
    {
        _context.BeginTransaction();

        try
        {
            EntityEntry<PromotionEntity> addedPromotion = await _context.Promotions.AddAsync(model, cancellationToken)
                .ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _context.CommitTransaction();

            return addedPromotion.Entity.Id;
        }
        catch (Exception ex)
        {
            _context.RollbackTransaction();
            _logger.LogError(ex.Message, ex);
            return 0;
        }
    }

    /// <summary>
    /// Asynchronously create promotion
    /// </summary>
    /// <param name="promotionId">Promotion Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains boolean if delete promotion succeed or not</returns>
    public async Task<bool> DeletePromotion(int promotionId, CancellationToken cancellationToken = default)
    {
        _context.BeginTransaction();

        try
        {
            PromotionEntity? promotion = _context.Promotions.FirstOrDefault(x => x.Id == promotionId);

            if (promotion != null)
            {
                promotion.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            _context.CommitTransaction();

            return true;
        }
        catch (Exception ex)
        {
            _context.RollbackTransaction();
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    /// <summary>
    /// Asynchronously get promotion detail
    /// </summary>
    /// <param name="promotionId">Promotion Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains promotion detail</returns>
    public async Task<PromotionEntity?> GetPromotionDetail(int promotionId, CancellationToken cancellationToken = default)
    {
        PromotionEntity? dbPromotion = _context.Promotions
            .AsNoTracking()
            .Include(x => x.StayPromotions)
            .FirstOrDefault(x => !x.IsDeleted && x.Id == promotionId);

        return await Task.FromResult(dbPromotion).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously get list of active promotions
    /// </summary>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Number of return value</param>
    /// <param name="orderBy">Order list result by Descending/Ascending</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains list of promotions</returns>
    public async Task<IReadOnlyCollection<PromotionEntity>> GetPromotions(int pageIndex = 0,
        int pageSize = 10,
        OrderBy orderBy = OrderBy.Descending, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<PromotionEntity> dbPromotions = _context.Promotions
            .AsNoTracking()
            .Include(x => x.StayPromotions)
            .Where(x => !x.IsDeleted);

        switch (orderBy)
        {
            case OrderBy.Ascending:
                {
                    dbPromotions = dbPromotions.OrderBy(x => x.CreatedDate);
                    break;
                }
            case OrderBy.Descending:
                {
                    dbPromotions = dbPromotions.OrderByDescending(x => x.CreatedDate);
                    break;
                }
            default:
                {
                    dbPromotions = dbPromotions.OrderByDescending(x => x.CreatedDate);
                    break;
                }
        }

        dbPromotions = dbPromotions.Skip(pageIndex * pageSize)
            .Take(pageSize);

        List<PromotionEntity> promotionEntities = await dbPromotions
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return new ReadOnlyCollection<PromotionEntity>(promotionEntities);
    }

    /// <summary>
    /// Asynchronously get list of active promotions by Stay
    /// </summary>
    /// <param name="stayId">Stay Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains list of promotions</returns>
    public async Task<IReadOnlyCollection<PromotionEntity>> GetPromotionsByStay(int stayId, CancellationToken cancellationToken = default)
    {
        List<PromotionEntity> dbPromotions = await _context.Promotions
            .AsNoTracking()
            .Include(x => x.StayPromotions.Where(y => y.StayId == stayId))
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new ReadOnlyCollection<PromotionEntity>(dbPromotions);
    }

    /// <summary>
    /// Asynchronously update promotion
    /// </summary>
    /// <param name="model">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains boolean if update promotion succeed or not</returns>
    public async Task<bool> UpdatePromotion(PromotionEntity model, CancellationToken cancellationToken = default)
    {
        _context.BeginTransaction();
        
        try
        {
            PromotionEntity? promotion = _context.Promotions.FirstOrDefault(x => x.Id == model.Id);

            if (promotion == null)
            {
                return false;
            }

            List<StayPromotionEntity> stayPromotions = [.. _context.StayPromotions.Where(x => x.PromotionId == model.Id && !x.IsDeleted)];
            _context.StayPromotions.RemoveRange(stayPromotions);

            _context.StayPromotions.AddRange(model.StayPromotions);

            _mapper.Map(model, promotion);

            _context.Promotions.Update(promotion);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _context.CommitTransaction();

            return true;
        }
        catch(Exception ex)
        {
            _context.RollbackTransaction();
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
