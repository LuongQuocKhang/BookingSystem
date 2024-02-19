using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IStayRepository
{
    Task<int> CreateStay(StayEntity model, CancellationToken cancellationToken = default);

    Task<bool> AddStayToTrip(int stayId, int tripId, CancellationToken cancellationToken = default);

    Task<bool> DeleteStay(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StayEntity>> GetStays(CancellationToken cancellationToken = default);

    Task<StayEntity?> GetStayById(int id, CancellationToken cancellationToken = default);

    Task<bool> ReviewStay(StayReviewEntity model, CancellationToken cancellationToken = default);

    Task<bool> SaveStayToWishList(StayWishListEntity wishList, CancellationToken cancellationToken = default);

    Task<bool> ShareStay(int stayId, IEnumerable<int> userIds, CancellationToken cancellationToken = default);

    Task<bool> UpdateStay(StayEntity model, CancellationToken cancellationToken = default);
}
