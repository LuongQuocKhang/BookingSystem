using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using AutoMapper;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.ReviewStay;

public class ReviewStayCommandHandler : IRequestHandler<ReviewStayCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    private readonly IMapper _mapper;
    public ReviewStayCommandHandler(IStayRepository stayRepository, IMapper mapper)
    {
        _stayRepository = stayRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(ReviewStayCommand request, CancellationToken cancellationToken)
    {
        ReviewStayViewModel model = _mapper.Map<ReviewStayViewModel>(request);
        await _stayRepository.ReviewStayAsync(request.StayId, model, request.UserId).ConfigureAwait(false);

        return true;
    }
}
