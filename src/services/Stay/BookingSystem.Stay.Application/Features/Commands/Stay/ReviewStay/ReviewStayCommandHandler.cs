using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.Dto;
using AutoMapper;
using MediatR;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.ReviewStay;

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
        StayReviewEntity model = _mapper.Map<StayReviewEntity>(request);
        await _stayRepository.ReviewStay(model).ConfigureAwait(false);

        return true;
    }
}
