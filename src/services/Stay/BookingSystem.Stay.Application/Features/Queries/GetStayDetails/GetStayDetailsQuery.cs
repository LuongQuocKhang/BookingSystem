using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Queries.GetStayDetails;

public class GetStayDetailsQuery : IRequest<StayDetailsViewModel>
{
    public int StayId { get; set; }
}
