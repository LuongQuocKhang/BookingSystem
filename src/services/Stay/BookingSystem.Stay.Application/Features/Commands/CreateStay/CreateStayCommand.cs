using BookingSystem.Stay.Application.ViewModel;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.CreateStay;


public class CreateStayCommand : IRequest<int>
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
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

    public List<AmenityViewModel> Amenities { get; set; }
    public List<RoomRateViewModel> RoomRates { get; set; }
    public List<StayAvailabilityViewModel> StayAvailability { get; set; }
    public List<StayImageViewModel> StayImages { get; set; }
}
