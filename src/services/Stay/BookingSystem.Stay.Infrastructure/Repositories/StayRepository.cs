using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;

namespace BookingSystem.Stay.Infrastructure.Repositories;

public class StayRepository : IStayRepository
{
    public Task AddStayToTripAsync(int stayId, int tripId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateStayAsync(CreateStayViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStayAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StayViewModel>> GetStaysAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StayDetailsViewModel> GetStaysByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task ReviewStayAsync(int stayId, ReviewStayViewModel model, int userId)
    {
        throw new NotImplementedException();
    }

    public Task SaveStayToWishListAsync(int stayId, int wishlistId)
    {
        throw new NotImplementedException();
    }

    public Task ShareStayAsync(int stayId, ShareStayViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStayAsync(StayDetailsViewModel model)
    {
        throw new NotImplementedException();
    }
}
