using BookingSystem.Stay.Application.Contracts.Persistance;
using AutoMapper;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.ShareStay;

public class ShareStayCommandHandler(IStayRepository stayRepository, IMapper mapper) : IRequestHandler<ShareStayCommand, bool>
{
    private readonly IStayRepository _stayRepository = stayRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> Handle(ShareStayCommand request, CancellationToken cancellationToken)
    {
        await _stayRepository.ShareStay(request.StayId, request.Recipients).ConfigureAwait(false);

        return true;
    }
}
