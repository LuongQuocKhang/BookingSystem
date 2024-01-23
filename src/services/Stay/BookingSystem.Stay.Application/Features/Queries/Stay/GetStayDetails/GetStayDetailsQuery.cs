using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Queries.Stay.GetStayDetails;

public class GetStayDetailsQuery : IRequest<StayDetailsViewModel>
{
    public int StayId { get; set; }
}
