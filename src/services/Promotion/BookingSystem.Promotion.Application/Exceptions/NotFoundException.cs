﻿using System.Runtime.Serialization;

namespace BookingSystem.Promotion.Application.Exceptions;

/// <summary>
/// Represents errors when a resource is not found.
/// </summary>
[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}