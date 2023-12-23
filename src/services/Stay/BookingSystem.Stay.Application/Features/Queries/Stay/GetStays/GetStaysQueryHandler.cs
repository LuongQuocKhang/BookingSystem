using AutoMapper;
using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Queries.Stay.GetStays;

public class GetStaysQueryHandler(IStayRepository stayRepository, IMapper mapper) 
    : IRequestHandler<GetStaysQuery, IReadOnlyCollection<StayViewModel>>
{
    private readonly IStayRepository _stayRepository = stayRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<StayViewModel>> Handle(GetStaysQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<StayEntity> stays = await _stayRepository.GetStays()
            .ConfigureAwait(false);

        return _mapper.Map<IReadOnlyCollection<StayViewModel>>(stays);
    }
}
