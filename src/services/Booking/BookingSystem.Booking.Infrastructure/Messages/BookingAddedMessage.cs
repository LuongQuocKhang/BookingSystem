namespace BookingSystem.Booking.Infrastructure.Messages;

using BookingSystem.Messages.Booking;

public class BookingAddedMessage : IBookingAddedMessage
{
    public int StayId { get; set; }

    public int UserId { get; set; }

    public int BookingId { get; set; }
}
