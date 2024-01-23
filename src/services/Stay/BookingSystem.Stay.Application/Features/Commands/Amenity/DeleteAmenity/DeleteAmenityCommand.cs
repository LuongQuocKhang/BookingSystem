using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.DeleteAmenity;

public class DeleteAmenityCommand : IRequest<bool>
{
    public int Id { get; set; }
}
