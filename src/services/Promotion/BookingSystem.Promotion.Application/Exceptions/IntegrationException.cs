using System.Runtime.Serialization;

namespace BookingSystem.Promotion.Application.Exceptions;


/// <summary>
/// Represents errors related to an external integration or service.
/// </summary>
[Serializable]
public class IntegrationException : Exception
{
    public IntegrationException()
    {
    }

    public IntegrationException(string? message) : base(message)
    {
    }

    public IntegrationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected IntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
