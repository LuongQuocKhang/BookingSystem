using BookingSystem.Stay.Application.Contracts.Persistance;
using AutoMapper;
using MediatR;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.CreateStay
{
    public class CreateStayCommandHandler(IStayRepository stayRepository, IMapper mapper) : IRequestHandler<CreateStayCommand, int>
    {
        private readonly IStayRepository _stayRepository = stayRepository;

        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateStayCommand request, CancellationToken cancellationToken)
        {
            StayEntity model = _mapper.Map<StayEntity>(request);

            int stayId = await _stayRepository.CreateStay(model, cancellationToken).ConfigureAwait(false);

            return stayId;
        }
    }
}
