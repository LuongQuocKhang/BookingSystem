using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using AutoMapper;
using MediatR;
using BookingSystem.Stay.Application.Features.Commands.CreateStay;

namespace BookingSystem.Stay.Application.Handlers.Commands.CreateStay
{
    public class CreateStayCommandHandler : IRequestHandler<CreateStayCommand, int>
    {
        private readonly IStayRepository _stayRepository;
        private readonly IMapper _mapper;
        public CreateStayCommandHandler(IStayRepository stayRepository, IMapper mapper)
        {
            _stayRepository = stayRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStayCommand request, CancellationToken cancellationToken)
        {
            CreateStayViewModel model = _mapper.Map<CreateStayViewModel>(request);
            int stayId = await _stayRepository.CreateStayAsync(model).ConfigureAwait(false);

            return stayId;
        }
    }
}
