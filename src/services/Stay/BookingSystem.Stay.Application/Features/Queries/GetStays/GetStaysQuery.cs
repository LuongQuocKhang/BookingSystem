using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Queries.GetStays;

public class GetStaysQuery : IRequest<IEnumerable<StayViewModel>>
{
}
