namespace BookingSystem.Stay.Application.ViewModel;

public class HostViewModel
{
    public int Id { get; set; }
    public int StayId { get; set; }

    public string? Name { get; set; } = string.Empty;

    public int TotalPlace { get; set; }

    public float ResponeRate { get; set; }

    public string? Note { get; set; }
}
