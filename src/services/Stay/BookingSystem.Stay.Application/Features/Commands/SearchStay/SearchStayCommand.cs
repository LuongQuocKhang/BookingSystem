using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.SearchStay;

public class SearchStayCommand : IRequest<IEnumerable<StayViewModel>>
{
}
