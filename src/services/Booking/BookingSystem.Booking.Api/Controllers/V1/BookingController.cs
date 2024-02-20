using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Booking.Api.Controllers.V1;

/// <summary>
/// Booking Management
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/bookings")]
[Authorize]
public class BookingController(IMediator mediator, ILogger<BookingController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<BookingController> _logger = logger;
}
