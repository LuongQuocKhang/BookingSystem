using BookingSystem.Promotion.Domain.Entities;
using BookingSystem.Promotion.Application.Constant;

namespace BookingSystem.Promotion.Application.Abstractions;

/// <summary>
/// Promotion Repository
/// </summary>
public interface IPromotionRepository
{
    /// <summary>
    /// Asynchronously create promotion
    /// </summary>
    /// <param name="model">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains created promotion id</returns>
    Task<int> CreatePromotion(PromotionEntity model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously get list of active promotions
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains list of promotions</returns>
    Task<IReadOnlyCollection<PromotionEntity>> GetPromotions(int pageIndex = 0,
        int pageSize = 10,
        OrderBy orderBy = OrderBy.Descending,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously get list of active promotions by stay
    /// </summary>
    /// <param name="stayId">Stay Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains list of promotions</returns>
    Task<IReadOnlyCollection<PromotionEntity>> GetPromotionsByStay(int stayId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously get promotion detail
    /// </summary>
    /// <param name="promotionId">Promotion Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains promotion detail</returns>
    Task<PromotionEntity?> GetPromotionDetail(int promotionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously update promotion
    /// </summary>
    /// <param name="model">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains boolean if update promotion succeed or not</returns>
    Task<bool> UpdatePromotion(PromotionEntity model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously create promotion
    /// </summary>
    /// <param name="promotionId">Promotion Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains boolean if delete promotion succeed or not</returns>
    Task<bool> DeletePromotion(int promotionId, CancellationToken cancellationToken = default);
}
