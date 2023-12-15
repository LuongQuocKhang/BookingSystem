using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using AutoMapper;
using MediatR;
using BookingSystem.Stay.Application.Features.Commands.CreateStay;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Handlers.Commands.CreateStay
{
    public class CreateStayCommandHandler(IStayRepository stayRepository, IMapper mapper) : IRequestHandler<CreateStayCommand, int>
    {
        private readonly IStayRepository _stayRepository = stayRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateStayCommand request, CancellationToken cancellationToken)
        {
            Stays model = _mapper.Map<Stays>(request);

            int stayId = await _stayRepository.CreateStay(model).ConfigureAwait(false);

            return stayId;
        }
    }
}
