using Asp.Versioning;
using BookingSystem.Stay.Application.Handlers.Queries.GetStays;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BookingSystem.Stay.Application.ViewModel;

namespace BookingSystem.Stay.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class AmenitiesController(IMediator mediator, ILogger<AmenitiesController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AmenitiesController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StayViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> GetAmenities()
    {
        IEnumerable<StayViewModel> result = await _mediator.Send(new GetStaysQuery());
        return Ok(result);
    }
}
