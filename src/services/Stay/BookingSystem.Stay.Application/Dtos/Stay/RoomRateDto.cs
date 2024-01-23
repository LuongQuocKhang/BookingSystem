namespace BookingSystem.Stay.Application.Dto;

public class RoomRateDto
{
    public string?   Name { get; set; } // Monday - Thursday, Rent by month

    public string? Value { get; set; } // $199, -8.34 %

    public int StayId { get; set; }
}
