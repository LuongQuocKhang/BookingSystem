using System.Runtime.Serialization;

namespace BookingSystem.Booking.Application.Exceptions;

[Serializable]
public class AccessDeniedException : Exception
{
    public AccessDeniedException()
    {
    }

    public AccessDeniedException(string? message) : base(message)
    {
    }

    public AccessDeniedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

