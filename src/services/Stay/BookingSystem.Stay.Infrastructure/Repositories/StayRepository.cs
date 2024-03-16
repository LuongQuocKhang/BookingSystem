using AutoMapper;
using BookingSystem.Stay.Application.Constant;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.Exceptions;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Abstractions;
using BookingSystem.Stay.Infrastructure.Constant;
using BookingSystem.Stay.Infrastructure.GrpcServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class StayRepository(IStayDbContext context, 
    IMapper mapper, 
    ILogger<StayRepository> logger,
    IPromotionGrpcService promotionGrpcService)
    : IStayRepository
{
    private readonly IStayDbContext _context = context;

    private readonly IMapper _mapper = mapper;

    private readonly ILogger<StayRepository> _logger = logger;

    private readonly IPromotionGrpcService _promotionGrpcService = promotionGrpcService;

    /// <summary>
    /// Asynchronously add stay to trip list
    /// </summary>
    /// <param name="stayId">Id of Stay</param>
    /// <param name="tripId">Id of Trip</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can add or not</returns>
    public Task<bool> AddStayToTrip(int stayId, int tripId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously create new stay
    /// </summary>
    /// <param name="model">new stay fields</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains id of new stay</returns>
    public async Task<int> CreateStay(StayEntity model, CancellationToken cancellationToken = default)
    {
        try
        {
            EntityEntry<StayEntity> addedStay = await _context.Stays.AddAsync(model, cancellationToken)
                .ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return addedStay.Entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return int.MinValue;
        }
    }

    /// <summary>
    /// Asynchronously delete stay
    /// </summary>
    /// <param name="id">id of delete stay</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can delete or not</returns>
    public async Task<bool> DeleteStay(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (id <= 0) return false;

            StayEntity? stay = _context.Stays.FirstOrDefault(x => x.Id == id);

            if (stay != null)
            {
                stay.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    /// <summary>
    /// Asynchronously get list of active Stays
    /// </summary>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Number of return value</param>
    /// <param name="orderBy">Order list result by Descending/Ascending</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains list of stays</returns>
    public async Task<IReadOnlyCollection<StayEntity>> GetStays(int pageIndex = 0, 
        int pageSize = 10, 
        OrderBy orderBy = OrderBy.Descending, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<StayEntity> dbStays = _context.Stays
            .AsNoTracking()
            .Include(x => x.RoomRates)
            .Include(x => x.StayAmenities!)
                .ThenInclude(x => x.Amenity)
            .Include(x => x.StayImages)
            .Include(x => x.StayTags)
            .Where(x => !x.IsDeleted)
            .Select(x => new StayEntity()
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name,
                NumberOfBathrooms = x.NumberOfBathrooms,
                NumberOfBedrooms = x.NumberOfBedrooms,
                NumberOfBeds = x.NumberOfBeds,
                NumberOfGuests = x.NumberOfGuests,
                PricePerNight = x.PricePerNight,
                Rating = x.Rating,
                StayAmenities = x.StayAmenities,
                RoomRates = x.RoomRates,
                StayImages = x.StayImages,
                StayTags = x.StayTags
            });

        switch(orderBy)
        {
            case OrderBy.Ascending:
                {
                    dbStays = dbStays.OrderBy(x => x.CreatedDate);
                    break;
                }
            case OrderBy.Descending:
                {
                    dbStays = dbStays.OrderByDescending(x => x.CreatedDate);
                    break;
                }
        }

        dbStays = dbStays.Skip(pageIndex * pageSize)
            .Take(pageSize);

        List<StayEntity> filteredStayEntities = await dbStays.ToListAsync(cancellationToken).ConfigureAwait(false);

        foreach (StayEntity stay in filteredStayEntities)
        {
            await GetPromotionForStay(stay, cancellationToken).ConfigureAwait(false);
        }

        var stayViews = new ReadOnlyCollection<StayEntity>(filteredStayEntities);

        return stayViews;
    }

    /// <summary>
    /// Asynchronously get stay by Id
    /// </summary>
    /// <param name="id">id of stay</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains stay information </returns>
    public async Task<StayEntity?> GetStayById(int id, CancellationToken cancellationToken = default)
    {
        StayEntity? stay = _context.Stays
            .AsNoTracking()
            .Include(x => x.StayTags)
            .Include(x => x.StayAmenities!)
                .ThenInclude(x => x.Amenity)
            .Include(x => x.RoomRates)
            .Include(x => x.StayUnAvailability)
            .Include(x => x.StayReviews)
            .Include(x => x.StayImages)
            .Include(x => x.StayTags)
            .Include(x => x.Host)
            .FirstOrDefault(x => x.Id == id
                && !x.IsDeleted) ?? throw new NotFoundException($"Stay : {id} not found.");

        await GetPromotionForStay(stay, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(stay).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously review stay
    /// </summary>
    /// <param name="model">contains rating, comment, id of stay </param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can review or not</returns>
    public async Task<bool> ReviewStay(StayReviewEntity model, CancellationToken cancellationToken = default)
    {
        EntityEntry<StayReviewEntity>? review = await _context.StayReviews.AddAsync(model, cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return review != null;
    }

    /// <summary>
    /// Asynchronously add stay to wish list
    /// </summary>
    /// <param name="model">contains rating, comment, id of stay </param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can review or not</returns>
    public async Task<bool> SaveStayToWishList(StayWishListEntity wishList, CancellationToken cancellationToken = default)
    {
        EntityEntry<StayWishListEntity>? stay = await _context.StayWishLists.AddAsync(wishList, cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return stay != null;
    }

    /// <summary>
    /// Asynchronously share stay to other people
    /// </summary>
    /// <param name="stayId">id of shared stay </param>
    /// <param name="userIds">id of received users </param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can share or not</returns>
    public async Task<bool> ShareStay(int stayId, IEnumerable<int> userIds, CancellationToken cancellationToken = default)
    {
        foreach (int userId in userIds)
        {
            StayShareEntity model = new()
            {
                StayId = stayId,
                UserId = userId
            };

            await _context.StayShares.AddAsync(model, cancellationToken).ConfigureAwait(false);
        }
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return true;
    }

    /// <summary>
    /// Asynchronously update stay information
    /// </summary>
    /// <param name="model">stay information </param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can update or not</returns>
    public async Task<bool> UpdateStay(StayEntity model, CancellationToken cancellationToken = default)
    {
        _context.BeginTransaction();

        try
        {
            StayEntity? stayDb = await GetStayById(model.Id).ConfigureAwait(false);

            if (stayDb == null) return false;

            #region Update Stay Images
            if (model.StayImages != null)
            {
                if (stayDb.StayImages != null && stayDb.StayImages.Count != 0)
                {
                    _context.StayImages.RemoveRange(stayDb.StayImages);
                }

                _context.StayImages.AddRange(model.StayImages);
            }
            #endregion

            #region Update Room Rates

            if (model.RoomRates != null)
            {
                if (stayDb.RoomRates != null && stayDb.RoomRates.Count != 0)
                {
                    _context.RoomRates.RemoveRange(stayDb.RoomRates);
                }

                _context.RoomRates.AddRange(model.RoomRates);
            }
            #endregion

            #region Update Amenities
            if (model.StayAmenities != null)
            {
                if (stayDb.StayAmenities != null && stayDb.StayAmenities.Count != 0)
                {
                    _context.StayAmenities.RemoveRange(stayDb.StayAmenities);
                }

                _context.StayAmenities.AddRange(model.StayAmenities);
            }
            #endregion

            #region Update StayUnAvailability

            if (model.StayUnAvailability != null)
            {
                if (stayDb.StayUnAvailability != null && stayDb.StayUnAvailability.Count != 0)
                {
                    _context.StayUnAvailability.RemoveRange(stayDb.StayUnAvailability);
                }

                _context.StayUnAvailability.AddRange(model.StayUnAvailability);
            }

            #endregion

            #region Update StayTags

            if (model.StayTags != null)
            {
                if (stayDb.StayTags != null && stayDb.StayTags.Count != 0)
                {
                    _context.StayTags.RemoveRange(stayDb.StayTags);
                }

                _context.StayTags.AddRange(model.StayTags);
            }

            #endregion

            _mapper.Map(model, stayDb);

            _context.Stays.Update(stayDb);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _context.CommitTransaction();

            return stayDb != null;
        }
        catch (Exception ex)
        {
            _context.RollbackTransaction();
            _logger.LogError($"Error When Update Stay : {ex.Message}", ex);
            return false;
        }
    }

    /// <summary>
    /// Asynchronously get promotion of stay
    /// </summary>
    /// <param name="stay">stay information </param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result if can update or not</returns>
    private async Task GetPromotionForStay(StayEntity stay, CancellationToken cancellationToken = default)
    {
        var promotions = await _promotionGrpcService.GetPromotions(stay.Id).ConfigureAwait(false);

        foreach (var promotion in promotions)
        {
            switch (promotion.DiscountType)
            {
                case (int)DiscountType.PRICE:
                    {
                        if (stay.PricePerNight - promotion.PriceDiscount > 0)
                        {
                            stay.PricePerNight -= promotion.PriceDiscount;
                        }

                        break;
                    }
                case (int)DiscountType.PERCENTAGE:
                    {
                        double discountPrice = stay.PricePerNight * (promotion.PercentageDiscount / 100);

                        if (stay.PricePerNight - discountPrice > 0)
                        {
                            stay.PricePerNight -= discountPrice;
                        }

                        break;
                    }
            }
        }
    }
}
