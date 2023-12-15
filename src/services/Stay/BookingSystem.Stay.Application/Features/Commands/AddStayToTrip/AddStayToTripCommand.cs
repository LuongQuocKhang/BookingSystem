using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.AddStayToTrip;

public class AddStayToTripCommand : IRequest<bool>
{
    public int StayId { get; set; }
    public int TripId { get; set; }
}
