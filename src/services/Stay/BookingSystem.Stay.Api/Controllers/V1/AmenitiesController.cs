using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

#region commands, queries
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Application.Features.Commands.Amenity.CreateAmenity;
using BookingSystem.Stay.Application.Features.Commands.Amenity.UpdateAmenity;
using BookingSystem.Stay.Application.Features.Commands.Amenity.DeleteAmenity;
using BookingSystem.Stay.Application.Features.Queries.Amenity.GetAmenities;
using BookingSystem.Stay.Application.ViewModel.Amenity;
#endregion

namespace BookingSystem.Stay.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/amenities")]
[Authorize]
public class AmenitiesController(IMediator mediator, ILogger<AmenitiesController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AmenitiesController> _logger = logger;

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<AmenityViewModel>>> GetAmenities()
    {
        IReadOnlyCollection<AmenityViewModel> result = await _mediator.Send(new GetAmenitiesQuery());
        return Ok(result);
    }

    [HttpGet("{amenityId}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<AmenityViewModel>> GetAmenityDetail(int amenityId)
    {
        AmenityViewModel result = await _mediator.Send(new GetAmenityQuery()
        {
            Id = amenityId
        });
        return Ok(result);
    }

    [HttpPost("create-amenity")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> CreateAmenity(CreateAmenityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("update-amenity")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> UpdateAmenity(UpdateAmenityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{amenityId}/delete-amenity")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> DeleteAmenity(int amenityId)
    {
        var result = await _mediator.Send(new DeleteAmenityCommand()
        {
            Id = amenityId
        });
        return Ok(result);
    }
}
