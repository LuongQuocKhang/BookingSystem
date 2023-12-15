using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.ViewModel;

public class StayDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int NumberOfBeds { get; set; }
    public int NumberOfGuests { get; set; }
    public int NumberOfBaths { get; set; }
    public int NumberOfBeedrooms { get; set; }
    public string HostedBy { get; set; } = "";
    public string? HostedDate { get; set; }
    public string? Address { get; set; }
    public double Rating { get; set; }
    public double PricePerNight { get; set; }
    public string? StayInformation { get; set; }
    public double ServiceCharge { get; set; }
    public int HostId { get; set; }
    public string? CancellationPolicy { get; set; }
    public string? CheckInTime { get; set; }
    public string? CheckOutTime { get; set; }
    public string? SpecialNotes { get; set; }
    public string? AvatarImage { get; set; }
    public List<StayAmenity> Amenities { get; set; }
    public List<RoomRate> RoomRates { get; set; }
    public List<StayAvailability> StayAvailability { get; set; }
    public List<StayImage> StayImages { get; set; }
}
