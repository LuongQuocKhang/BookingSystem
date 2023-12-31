﻿using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class StayRepository(StayContext context, IMapper mapper, ILogger<StayRepository> logger) : IStayRepository
{
    private readonly StayContext _context = context;

    private readonly IMapper _mapper = mapper;

    private readonly ILogger<StayRepository> _logger = logger;

    public Task<bool> AddStayToTrip(int stayId, int tripId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateStay(Domain.Entities.StayEntity model, CancellationToken cancellationToken = default)
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

    public async Task DeleteStay(int id, CancellationToken cancellationToken = default)
    {
        StayEntity? stay = await _context.Stays.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (stay != null)
        {
            stay.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task<IReadOnlyCollection<StayEntity>> GetStays()
    {
        List<StayEntity> dbStays = await _context.Stays
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
            })
            .ToListAsync()
            .ConfigureAwait(false);

        var stayViews = new ReadOnlyCollection<StayEntity>(dbStays);

        return stayViews;
    }

    public async Task<StayEntity?> GetStaysById(int id)
    {
        StayEntity? stays = await _context.Stays
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
            .FirstOrDefaultAsync(x => x.Id == id 
                && !x.IsDeleted)
            .ConfigureAwait(false);

        return stays;
    }

    public async Task<bool> ReviewStay(StayReviewEntity model, CancellationToken cancellationToken = default)
    {
        EntityEntry<StayReviewEntity>? review = await _context.StayReviews.AddAsync(model, cancellationToken)
            .ConfigureAwait(false);
        return review != null;
    }

    public async Task<bool> SaveStayToWishList(StayWishListEntity wishList, CancellationToken cancellationToken = default)
    {
        EntityEntry<StayWishListEntity>? stay = await _context.StayWishLists.AddAsync(wishList, cancellationToken)
            .ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return stay != null;
    }

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

    public async Task<bool> UpdateStay(Domain.Entities.StayEntity model, CancellationToken cancellationToken = default)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            StayEntity? stayDb = await GetStaysById(model.Id).ConfigureAwait(false);

            if (stayDb != null)
            {
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

                _context.Entry(stayDb).State = EntityState.Modified;

                _context.Stays.Update(stayDb);

                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            transaction.Commit();

            return stayDb != null;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            _logger.LogError($"Error When Update Stay : {ex.Message}", ex);
            return false;
        }
    }
}
