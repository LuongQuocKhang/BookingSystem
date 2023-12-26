using BookingSystem.Stay.Application.Message;
using BookingSystem.Stay.Application.ViewModel.Amenity;

namespace BookingSystem.Stay.Application.Features.Queries.Amenity.GetAmenities;

public class GetAmenitiesQuery : ICachedQuery<IReadOnlyCollection<AmenityViewModel>>
{
    public string Key => nameof(GetAmenitiesQuery);

    public TimeSpan? Expiration { get; set; } = TimeSpan.FromMinutes(15);
}
