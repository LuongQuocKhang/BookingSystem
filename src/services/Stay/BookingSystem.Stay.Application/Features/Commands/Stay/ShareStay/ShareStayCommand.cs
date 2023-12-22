using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.ShareStay;

public class ShareStayCommand : IRequest<bool>
{
    public int StayId { get; set; }
    public List<int> Recipients { get; set; } = new List<int>();
}
