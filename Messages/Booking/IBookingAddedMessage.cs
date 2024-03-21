namespace BookingSystem.Messages.Booking;

public interface IBookingAddedMessage
{
    public int StayId { get; set; }

    public int UserId { get; set; }

    public int BookingId { get; set; }
}
