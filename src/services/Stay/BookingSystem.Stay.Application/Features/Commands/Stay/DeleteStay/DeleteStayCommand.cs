using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.DeleteStay;

public class DeleteStayCommand : IRequest<bool>
{
    public int StayId { get; set; }
}
