using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using AutoMapper;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.UpdateStay;

public class UpdateStayCommandHandler : IRequestHandler<UpdateStayCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    private readonly IMapper _mapper;
    public UpdateStayCommandHandler(IStayRepository stayRepository, IMapper mapper)
    {
        _stayRepository = stayRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateStayCommand request, CancellationToken cancellationToken)
    {
        StayDetailsViewModel model = _mapper.Map<StayDetailsViewModel>(request);
        await _stayRepository.UpdateStayAsync(model).ConfigureAwait(false);
        return true;
    }
}
