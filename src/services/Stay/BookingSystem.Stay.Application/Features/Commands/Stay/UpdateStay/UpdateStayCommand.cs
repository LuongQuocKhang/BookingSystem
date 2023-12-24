using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Application.Dtos.Stay;
using BookingSystem.Stay.Domain.Entities;
using MediatR;

namespace BookingSystem.Stay.Application.Features.Commands.Stay.UpdateStay;

public class UpdateStayCommand : IRequest<bool>
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

    public List<StayAmenityDto>? StayAmenities { get; set; }

    public List<RoomRateDto>? RoomRates { get; set; }

    public List<StayUnAvailabilityDto>? StayUnAvailability { get; set; }

    public List<StayImageDto>? StayImages { get; set; }

    public List<StayTagDto>? StayTags { get; set; }
}
