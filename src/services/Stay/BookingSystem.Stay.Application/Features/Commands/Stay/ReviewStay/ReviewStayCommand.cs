using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.ReviewStay;

public class ReviewStayCommand : IRequest<bool>
{
    public int StayId { get; set; }
    public int Rating { get; set; }
    public int UserId { get; set; }
    public string? Comment { get; set; }
}
