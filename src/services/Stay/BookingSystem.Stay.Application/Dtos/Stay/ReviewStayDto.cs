namespace BookingSystem.Stay.Application.Dto;

public class ReviewStayDto
{
    public int Rating { get; set; }

    public string? Comment { get; set; } = string.Empty;

    public int UserId { get; set; }
}
