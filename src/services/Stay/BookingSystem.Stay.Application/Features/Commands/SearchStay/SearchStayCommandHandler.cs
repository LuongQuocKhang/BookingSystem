using BookingSystem.Stay.Application.Contracts.Persistance;
using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.SearchStay;

public class SearchStayCommandHandler : IRequestHandler<SearchStayCommand, IEnumerable<StayViewModel>>
{
    private readonly IStayRepository _stayRepository;

    public SearchStayCommandHandler(IStayRepository stayRepository)
    {
        _stayRepository = stayRepository;
    }

    public Task<IEnumerable<StayViewModel>> Handle(SearchStayCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
