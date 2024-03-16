using AutoMapper;
using BookingSystem.Booking.Application.Exceptions;
using BookingSystem.Booking.Domain.Entities;
using BookingSystem.Booking.Infrastructure.Abstractions;
using MediatR;

namespace BookingSystem.Booking.Application.Features.Booking.Commands;

public class BookingStayCommandHandler(IStayGrpcService stayGrpcService, IMapper mapper, IBookingRepository bookingRepository) : IRequestHandler<BookingStayCommand, int>
{
    private readonly IStayGrpcService _stayGrpcService = stayGrpcService;

    private readonly IMapper _mapper = mapper;

    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<int> Handle(BookingStayCommand request, CancellationToken cancellationToken)
    {
        _ = await _stayGrpcService.GetStayById(request.StayId).ConfigureAwait(false) ?? throw new NotFoundException("Stay Not Found.");

        BookingEntity dto = _mapper.Map<BookingEntity>(request);

        int result = await _bookingRepository.BookStay(dto).ConfigureAwait(false);

        return result;
    }
}
