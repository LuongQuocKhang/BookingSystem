using BookingSystem.Stay.Application.Message;
using BookingSystem.Stay.Application.ViewModel.Amenity;

namespace BookingSystem.Stay.Application.Features.Queries.Amenity.GetAmenities;

public class GetAmenitiesQuery : ICachedQuery<IReadOnlyCollection<AmenityViewModel>>
{
    public int PazeIndex { get; set; }

    public int PazeSize { get; set; }

    public string Key => nameof(GetAmenitiesQuery);

    public TimeSpan? Expiration { get; set; } = TimeSpan.FromMinutes(15);
}
