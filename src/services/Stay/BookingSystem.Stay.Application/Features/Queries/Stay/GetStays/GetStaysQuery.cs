using BookingSystem.Stay.Application.Message;
using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Queries.Stay.GetStays;

public class GetStaysQuery : ICachedQuery<IEnumerable<StayViewModel>>
{
    public string Key => nameof(GetStaysQuery);

    public TimeSpan? Expiration { get; set; } = TimeSpan.FromMinutes(15);
}
