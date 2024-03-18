namespace BookingSystem.Booking.Infrastructure.Messages;

public class MasstransitEnvelop<T> where T : class
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public T Message { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
