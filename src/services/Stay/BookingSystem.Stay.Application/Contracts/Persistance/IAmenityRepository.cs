using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Contracts.Persistance;

public interface IAmenityRepository
{
    Task<IReadOnlyCollection<AmenityEntity>> GetAmenities();

    Task<AmenityEntity?> GetAmenity(int id);

    Task<bool> UpdateAmenity(AmenityEntity entity);

    Task<bool> CreateAmenity(AmenityEntity entity);

    Task<bool> DeleteAmenity(int id);
}
