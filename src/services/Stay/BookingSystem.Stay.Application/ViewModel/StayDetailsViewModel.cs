using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Domain.Entities;

namespace BookingSystem.Stay.Application.ViewModel;

public class StayDetailsViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; } = string.Empty;

    public int NumberOfBeds { get; set; }

    public int NumberOfGuests { get; set; }

    public int NumberOfBathrooms { get; set; }

    public int NumberOfBedrooms { get; set; }

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

    public List<StayAmenityViewModel>? StayAmenities { get; set; }

    public List<StayRoomRateViewModel>? RoomRates { get; set; }

    public List<StayUnAvailabilityViewModel>? StayUnAvailability { get; set; }

    public List<StayImageViewModel>? StayImages { get; set; }

    public List<StayReviewViewModel>? StayReviews { get; set; }

    public List<StayTagViewModel>? StayTags { get; set; }

    public HostViewModel? Host { get; set; }
}
