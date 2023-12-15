using BookingSystem.Stay.Application.ViewModel;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IStayRepository
{
    Task<IEnumerable<StayViewModel>> GetStaysAsync();

    Task<StayDetailsViewModel> GetStaysByIdAsync(int id);

    Task ReviewStayAsync(int stayId, ReviewStayViewModel model, int userId);

    Task ShareStayAsync(int stayId, ShareStayViewModel model);

    Task SaveStayToWishListAsync(int stayId, int wishlistId);

    Task AddStayToTripAsync(int stayId, int tripId);

    Task<int> CreateStayAsync(CreateStayViewModel model);

    Task UpdateStayAsync(StayDetailsViewModel model);

    Task DeleteStayAsync(int id);
}
