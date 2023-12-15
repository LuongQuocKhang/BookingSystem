using BookingSystem.Stay.Application.Contracts.Persistance;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.AddStayToTrip;

public class AddStayToTripCommandHandler : IRequestHandler<AddStayToTripCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    public AddStayToTripCommandHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }
    public async Task<bool> Handle(AddStayToTripCommand request, CancellationToken cancellationToken)
    {
        await _stayRepository.AddStayToTripAsync(request.StayId, request.TripId).ConfigureAwait(false);

        return true;
    }
}
