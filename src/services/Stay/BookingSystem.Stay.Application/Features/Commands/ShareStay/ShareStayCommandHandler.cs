using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using AutoMapper;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.ShareStay;

public class ShareStayCommandHandler : IRequestHandler<ShareStayCommand, bool>
{
    private readonly IStayRepository _stayRepository;
    private readonly IMapper _mapper;
    public ShareStayCommandHandler(IStayRepository stayRepository, IMapper mapper)
    {
        _stayRepository = stayRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(ShareStayCommand request, CancellationToken cancellationToken)
    {
        ShareStayViewModel model = _mapper.Map<ShareStayViewModel>(request);
        await _stayRepository.ShareStay(request.StayId, model).ConfigureAwait(false);

        return true;
    }
}
