using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.CreateAmenity;

public class CreateAmenityCommand : IRequest<int>
{
    public string? Name { get; set; }

    public string? Icon { get; set; }
}
