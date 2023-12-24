using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IStayRepository
{
    Task<int> CreateStay(Domain.Entities.StayEntity model, CancellationToken cancellationToken = default);

    Task<bool> AddStayToTrip(int stayId, int tripId, CancellationToken cancellationToken = default);

    Task DeleteStay(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StayEntity>> GetStays();

    Task<StayEntity?> GetStaysById(int id);

    Task<bool> ReviewStay(StayReviewEntity model, CancellationToken cancellationToken = default);

    Task<bool> SaveStayToWishList(StayWishListEntity wishList, CancellationToken cancellationToken = default);

    Task<bool> ShareStay(int stayId, IEnumerable<int> userIds, CancellationToken cancellationToken = default);

    Task<bool> UpdateStay(Domain.Entities.StayEntity model, CancellationToken cancellationToken = default);
}
