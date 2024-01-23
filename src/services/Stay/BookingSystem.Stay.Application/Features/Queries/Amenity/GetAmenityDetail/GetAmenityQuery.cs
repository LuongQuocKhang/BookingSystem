using BookingSystem.Stay.Application.Message;
using BookingSystem.Stay.Application.ViewModel.Amenity;

namespace BookingSystem.Stay.Application.Features.Queries.Amenity.GetAmenities;

public class GetAmenityQuery : IQuery<AmenityViewModel>
{
    public int Id { get; set; }
}
