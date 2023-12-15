using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Queries.GetStays;

public class GetStaysQueryHandler : IRequestHandler<GetStaysQuery, IEnumerable<StayViewModel>>
{
    private readonly IStayRepository _stayRepository;

    public GetStaysQueryHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }

    public async Task<IEnumerable<StayViewModel>> Handle(GetStaysQuery request, CancellationToken cancellationToken)
    {
        return await _stayRepository.GetStaysAsync().ConfigureAwait(false);
    }
}
