namespace BookingSystem.Booking.Infrastructure.ViewModel;

public class StayViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int NumberOfBeds { get; set; }

    public int NumberOfGuests { get; set; }

    public int NumberOfBathrooms { get; set; }

    public int NumberOfBedrooms { get; set; }

    public string? Address { get; set; }

    public double Rating { get; set; }

    public double PricePerNight { get; set; }
}
