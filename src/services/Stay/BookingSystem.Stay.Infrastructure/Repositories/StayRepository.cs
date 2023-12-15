using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
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
        List<StayEntity> dbStays = await _context.Stays.Where(x => !x.IsDeleted)
            .ToListAsync()
            .ConfigureAwait(false);

        IReadOnlyCollection<StayViewModel> stayViews = _mapper.Map<IReadOnlyCollection<StayViewModel>>(dbStays);

        return stayViews;
    }

    public async Task<StayDetailsViewModel> GetStaysById(int id)
    {
        StayEntity? stays = await _context.Stays.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        if (stays != null)
        {
            StayDetailsViewModel stayDetailsViews = _mapper.Map<StayDetailsViewModel>(stays);

            return stayDetailsViews;
        }

        return new StayDetailsViewModel();
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
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return stay != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
