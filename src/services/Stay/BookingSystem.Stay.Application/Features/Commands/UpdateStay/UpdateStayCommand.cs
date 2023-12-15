using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Handlers.Commands.UpdateStay;

public class UpdateStayCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;

    public int NumberOfBeds { get; set; }

    public int NumberOfGuests { get; set; }

    public int NumberOfBathrooms { get; set; }

    public int NumberOfBedrooms { get; set; }

    public int HostedBy { get; set; }

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

    public List<AmenityEntity> Amenities { get; set; }

    public List<RoomRateEntity> RoomRates { get; set; }

    public List<StayUnAvailabilityEntity> StayAvailability { get; set; }

    public List<StayImageEntity> StayImages { get; set; }
}
