using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IStayRepository
{
    Task<int> CreateStay(Domain.Entities.StayEntity model);

    Task<bool> AddStayToTrip(int stayId, int tripId);

    Task DeleteStay(int id);

    Task<IReadOnlyCollection<StayEntity>> GetStays();

    Task<StayEntity?> GetStaysById(int id);

    Task<bool> ReviewStay(StayReviewEntity model);

    Task<bool> SaveStayToWishList(StayWishListEntity wishList);

    Task<bool> ShareStay(int stayId, IEnumerable<int> userIds);

    Task<bool> UpdateStay(Domain.Entities.StayEntity model);
}
