using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.SearchStay;

public class SearchStayCommand : IRequest<IEnumerable<StayViewModel>>
{
}
