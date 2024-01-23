namespace BookingSystem.Stay.Application.Exceptions;

public class Error
{
    public string Code { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Target { get; set; } = string.Empty;

    public IEnumerable<Error> Details { get; set; } = Enumerable.Empty<Error>();
}
