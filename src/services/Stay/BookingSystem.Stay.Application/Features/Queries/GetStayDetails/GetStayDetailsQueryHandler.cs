using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Queries.GetStayDetails;

public class GetStayDetailsQueryHandler : IRequestHandler<GetStayDetailsQuery, StayDetailsViewModel>
{
    private readonly IStayRepository _stayRepository;

    public GetStayDetailsQueryHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }

    public async Task<StayDetailsViewModel> Handle(GetStayDetailsQuery request, CancellationToken cancellationToken)
    {
        StayDetailsViewModel stay = await _stayRepository.GetStaysById(request.StayId).ConfigureAwait(false);

        return stay;
    }
}
