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

/// <summary>
/// Amenities Management
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/amenities")]
[Authorize]
public class AmenitiesController(IMediator mediator, ILogger<AmenitiesController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AmenitiesController> _logger = logger;

    /// <summary>
    /// Get list of amenities
    /// </summary>
    /// <response code="200">Return List of Amenities </response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<AmenityViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyCollection<AmenityViewModel>>> GetAmenities(int pageIndex = 0, int pageSize = 10)
    {
        IReadOnlyCollection<AmenityViewModel> result = await _mediator.Send(new GetAmenitiesQuery()
        {
            PazeIndex = pageIndex,
            PazeSize = pageSize
        });
        return Ok(result);
    }

    /// <summary>
    /// Get amenity detail
    /// </summary>
    /// <response code="200">Return amenity details </response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpGet("{amenityId}")]
    [ProducesResponseType(typeof(AmenityViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AmenityViewModel>> GetAmenityDetail(int amenityId)
    {
        AmenityViewModel result = await _mediator.Send(new GetAmenityQuery()
        {
            Id = amenityId
        });
        return Ok(result);
    }

    /// <summary>
    /// Create amenity
    /// </summary>
    /// <response code="200">Amenity Id </response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost("create-amenity")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateAmenity(CreateAmenityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Update amenity
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPut("update-amenity")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> UpdateAmenity(UpdateAmenityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete amenity
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpDelete("{amenityId}/delete-amenity")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteAmenity(int amenityId)
    {
        var result = await _mediator.Send(new DeleteAmenityCommand()
        {
            Id = amenityId
        });
        return Ok(result);
    }
}
