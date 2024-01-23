using BookingSystem.Stay.Application.Message;
using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Queries.Stay.GetStays;

public class GetStaysQuery : IRequest<IReadOnlyCollection<StayViewModel>>
{
    public int PazeIndex { get; set; }

    public int PazeSize { get; set; }

    //public string Key => nameof(GetStaysQuery);

    //public TimeSpan? Expiration { get; set; } = TimeSpan.FromMinutes(15);
}
