using BookingSystem.Stay.Application.Message;
using BookingSystem.Stay.Application.ViewModel;

namespace BookingSystem.Stay.Application.Features.Queries.Stay.GetStays;

public class GetStaysQuery : ICachedQuery<IReadOnlyCollection<StayViewModel>>
{
    public string Key => nameof(GetStaysQuery);

    public TimeSpan? Expiration { get; set; } = TimeSpan.FromMinutes(15);
}
