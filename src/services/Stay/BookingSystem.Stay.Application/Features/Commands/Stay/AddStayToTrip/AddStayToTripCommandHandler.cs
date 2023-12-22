using BookingSystem.Stay.Application.Contracts.Persistance;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.AddStayToTrip;

public class AddStayToTripCommandHandler : IRequestHandler<AddStayToTripCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    public AddStayToTripCommandHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }
    public async Task<bool> Handle(AddStayToTripCommand request, CancellationToken cancellationToken)
    {
        await _stayRepository.AddStayToTrip(request.StayId, request.TripId).ConfigureAwait(false);

        return true;
    }
}
