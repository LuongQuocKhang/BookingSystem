using BookingSystem.Stay.Application.Contracts.Persistance;
using AutoMapper;
using MediatR;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Handlers.Commands.UpdateStay;

public class UpdateStayCommandHandler(IStayRepository stayRepository, IMapper mapper) : IRequestHandler<UpdateStayCommand, bool>
{
    private readonly IStayRepository _stayRepository = stayRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> Handle(UpdateStayCommand request, CancellationToken cancellationToken)
    {
        Stays model = _mapper.Map<Stays>(request);
        await _stayRepository.UpdateStay(model).ConfigureAwait(false);
        return true;
    }
}
