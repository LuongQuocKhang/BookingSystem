namespace BookingSystem.Stay.Application.ViewModel;

public class StayRoomRateViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; } // Monday - Thursday, Rent by month

    public string? Value { get; set; } // $199, -8.34 %

    public int StayId { get; set; }
}
