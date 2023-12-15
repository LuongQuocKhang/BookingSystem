using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IStayRepository
{
    Task<int> CreateStay(Stays model);

    Task<bool> AddStayToTrip(int stayId, int tripId);

    Task DeleteStay(int id);

    Task<IReadOnlyCollection<StayViewModel>> GetStays();

    Task<StayDetailsViewModel> GetStaysById(int id);

    Task<bool> ReviewStay(StayReview model);

    Task<bool> SaveStayToWishList(StayWishList wishList);

    Task<bool> ShareStay(int stayId, IEnumerable<int> userIds);

    Task<bool> UpdateStay(Stays model);
}
