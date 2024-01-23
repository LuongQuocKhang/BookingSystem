using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Amenity.UpdateAmenity;

public class UpdateAmenityCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Icon { get; set; }

    public bool IsDeleted { get; set; }
}
