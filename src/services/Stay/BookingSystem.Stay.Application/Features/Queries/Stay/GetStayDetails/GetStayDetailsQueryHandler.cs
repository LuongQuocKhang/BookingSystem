using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;
using MediatR;
using AutoMapper;

namespace BookingSystem.Stay.Application.Features.Queries.Stay.GetStayDetails;

public class GetStayDetailsQueryHandler(IStayRepository stayRepository, IMapper mapper)
    : IRequestHandler<GetStayDetailsQuery, StayDetailsViewModel?>
{
    private readonly IStayRepository _stayRepository = stayRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<StayDetailsViewModel?> Handle(GetStayDetailsQuery request, CancellationToken cancellationToken)
    {
        StayEntity? stay = await _stayRepository.GetStayById(request.StayId)
            .ConfigureAwait(false);

        StayDetailsViewModel? stayDetails = _mapper.Map<StayDetailsViewModel>(stay);

        return stayDetails;
    }
}
