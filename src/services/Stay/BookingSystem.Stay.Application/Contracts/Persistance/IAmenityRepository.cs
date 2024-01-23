using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IAmenityRepository
{
    Task<IReadOnlyCollection<AmenityEntity>> GetAmenities();

    Task<AmenityEntity?> GetAmenityDetail(int id);

    Task<bool> UpdateAmenity(AmenityEntity entity, CancellationToken cancellationToken = default);

    Task<int> CreateAmenity(AmenityEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAmenity(int id, CancellationToken cancellationToken = default);
}
