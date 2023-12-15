using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.DeleteStay;

public class DeleteStayCommand : IRequest<bool>
{
    public int StayId { get; set; }
}
