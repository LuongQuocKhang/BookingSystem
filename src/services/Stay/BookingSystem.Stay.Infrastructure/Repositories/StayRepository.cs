using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class StayRepository(StayContext context, IMapper mapper, ILogger<StayRepository> logger) : IStayRepository
{
    private readonly StayContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<StayRepository> _logger = logger;

    public Task<bool> AddStayToTrip(int stayId, int tripId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateStay(Domain.Entities.StayEntity model)
    {
        try
        {
            EntityEntry<StayEntity> addedStay = await _context.Stays.AddAsync(model)
            .ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return addedStay.Entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return int.MinValue;
        }
    }

    public async Task DeleteStay(int id)
    {
        StayEntity? stay = await _context.Stays.FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        if (stay != null)
        {
            stay.IsDeleted = true;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task<IReadOnlyCollection<StayViewModel>> GetStays()
    {
        List<StayViewModel> dbStays = await _context.Stays
            .AsNoTracking()
            .Include(x => x.RoomRates)
            .Include(x => x.Amenities)
            .Include(x => x.StayImages)
            .Include(x => x.StayTags)
            .Where(x => !x.IsDeleted)
            .Select(x => new StayViewModel()
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
                Amenities = _mapper.Map<List<StayAmenityViewModel>>(x.Amenities),
                RoomRates = _mapper.Map<List<RoomRateViewModel>>(x.RoomRates),
                StayImages = _mapper.Map<List<StayImageViewModel>>(x.StayImages),
                StayTags = _mapper.Map<List<StayTagViewModel>>(x.StayTags)
            })
            .ToListAsync()
            .ConfigureAwait(false);

        IReadOnlyCollection<StayViewModel> stayViews = _mapper.Map<IReadOnlyCollection<StayViewModel>>(dbStays);

        return stayViews;
    }

    public async Task<StayDetailsViewModel?> GetStaysById(int id)
    {
        StayEntity? stays = await _context.Stays
            .AsNoTracking()
            .Include(x => x.StayTags)
            .Include(x => x.Amenities!)
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

        if (stays != null)
        {
            StayDetailsViewModel stayDetailsViews = _mapper.Map<StayDetailsViewModel>(stays);

            return stayDetailsViews;
        }

        return null;
    }

    public async Task<bool> ReviewStay(StayReviewEntity model)
    {
        EntityEntry<StayReviewEntity>? review = await _context.StayReviews.AddAsync(model)
            .ConfigureAwait(false);
        return review != null;
    }

    public async Task<bool> SaveStayToWishList(StayWishListEntity wishList)
    {
        EntityEntry<StayWishListEntity>? stay = await _context.StayWishLists.AddAsync(wishList)
            .ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return stay != null;
    }

    public async Task<bool> ShareStay(int stayId, IEnumerable<int> userIds)
    {
        foreach (int userId in userIds)
        {
            StayShareEntity model = new()
            {
                StayId = stayId,
                UserId = userId
            };

            _context.StayShares.Add(model);
        }
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return true;
    }

    public async Task<bool> UpdateStay(Domain.Entities.StayEntity model)
    {
        try
        {
            EntityEntry<StayEntity> stay = _context.Stays.Update(model);

            stay.State = EntityState.Modified;

            _context.Attach(stay);

            await _context.SaveChangesAsync()
                .ConfigureAwait(false);

            return stay != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
