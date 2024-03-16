using BookingSystem.Booking.Application.Features.Booking.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

    /// <summary>
    /// Book stay
    /// </summary>
    /// <response code="200">booking Id</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("book-stay")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<int>> CreateStay(BookingStayCommand command)
    {
        int bookingId = await _mediator.Send(command);
        return Ok(bookingId);
    }

}
