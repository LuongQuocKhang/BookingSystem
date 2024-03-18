namespace BookingSystem.Booking.Infrastructure.Messages;

public class BookingAddedMessage
{
    public int StayId { get; set; }

    public int UserId { get; set; }

    public int BookingId { get; set; }
}
