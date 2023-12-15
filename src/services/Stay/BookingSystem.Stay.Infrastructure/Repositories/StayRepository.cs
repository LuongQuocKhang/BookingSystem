using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;
using BookingSystem.Stay.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class StayRepository(StayContext context, IMapper mapper) : IStayRepository
{
    private readonly StayContext _context = context;
    private readonly IMapper _mapper = mapper;

    public Task<bool> AddStayToTrip(int stayId, int tripId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateStay(Stays model)
    {
        EntityEntry<Stays> addedStay = await _context.Stays.AddAsync(model)
            .ConfigureAwait(false);

        return addedStay.Entity.Id;
    }

    public async Task DeleteStay(int id)
    {
        Stays? stay = await _context.Stays.FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        if(stay != null)
        {
            stay.IsDeleted = true;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task<IReadOnlyCollection<StayViewModel>> GetStays()
    {
        List<Stays> dbStays = await _context.Stays.Where(x => !x.IsDeleted)
            .ToListAsync()
            .ConfigureAwait(false);

        IReadOnlyCollection<StayViewModel> stayViews = _mapper.Map<IReadOnlyCollection<StayViewModel>>(dbStays);

        return stayViews;
    }

    public async Task<StayDetailsViewModel> GetStaysById(int id)
    {
        Stays? stays = await _context.Stays.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        if (stays != null)
        {
            StayDetailsViewModel stayDetailsViews = _mapper.Map<StayDetailsViewModel>(stays);

            return stayDetailsViews;
        }

        return new StayDetailsViewModel();
    }

    public async Task<bool> ReviewStay(StayReview model)
    {
        EntityEntry<StayReview>? review = await _context.StayReviews.AddAsync(model)
            .ConfigureAwait(false);
        return review != null;
    }

    public Task<bool> SaveStayToWishList(StayWishList wishList)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ShareStay(int stayId, IEnumerable<int> userIds)
    {
        foreach (int userId in userIds)
        {
            StayShare model = new StayShare()
            {
                StaysId = stayId,
                UserId = userId
            };

            _context.StayShares.Add(model);
        }
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<bool> UpdateStay(Stays model)
    {
        throw new NotImplementedException();
    }
}
