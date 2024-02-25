#region Using Commands, queries
using BookingSystem.Stay.Application.Features.Commands.Stay.ReviewStay;
using BookingSystem.Stay.Application.Features.Commands.Stay.SearchStay;
using BookingSystem.Stay.Application.Features.Commands.Stay.CreateStay;
using BookingSystem.Stay.Application.Features.Commands.Stay.UpdateStay;
using BookingSystem.Stay.Application.Features.Queries.Stay.GetStays;
using BookingSystem.Stay.Application.Features.Queries.Stay.GetStayDetails;
using BookingSystem.Stay.Application.Features.Commands.Stay.ShareStay;
using BookingSystem.Stay.Application.Features.Commands.Stay.SaveStayToWishlist;
using BookingSystem.Stay.Application.Features.Commands.Stay.DeleteStay;
using BookingSystem.Stay.Application.Features.Commands.Stay.AddStayToTrip;
#endregion

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using BookingSystem.Stay.Application.ViewModel;
using BookingSystem.Stay.Application.Dto;
using BookingSystem.Stay.Application.Constant;

namespace BookingSystem.Stay.Api.Controllers.V1;


/// <summary>
/// Stay Management
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class StaysController(ILogger<StaysController> logger, IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<StaysController> _logger = logger;

    /// <summary>
    /// Get list of stays
    /// </summary>
    /// <response code="200">List of stays</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StayViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> GetStays(int pageIndex = 0, int pageSize = 10, OrderBy orderBy = OrderBy.Descending)
    {
        IEnumerable<StayViewModel> result = await _mediator.Send(new GetStaysQuery() { 
            PazeIndex = pageIndex,
            PazeSize = pageSize,
            OrderBy = orderBy
        });
        return Ok(result);
    }

    /// <summary>
    /// Get stay detail
    /// </summary>
    /// <response code="200">Stay detail</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpGet("{stayId}")]
    [ProducesResponseType(typeof(StayDetailsViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StayDetailsViewModel>> GetStayDetails(int stayId)
    {
        StayDetailsViewModel result = await _mediator.Send(new GetStayDetailsQuery()
        {
            StayId = stayId
        });
        return Ok(result);
    }

    /// <summary>
    /// Review stay
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("{stayId}/review-stay")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> ReviewStay(int stayId, ReviewStayDto model)
    {
        ReviewStayCommand command = new()
        {
            Comment = model.Comment,
            Rating = model.Rating,
            StayId = stayId,
            UserId = 1
        };
        bool result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Share stay to other user
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("{stayId}/share-stay")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> ShareStay(int stayId, List<int> Recipients)
    {
        bool result = await _mediator.Send(new ShareStayCommand()
        {
            Recipients = Recipients,
            StayId = stayId
        });
        return Ok(result);
    }

    /// <summary>
    /// Add stay to wishlist
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("{stayId}/add-to-wishlist/{wishlistId}")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> SaveStayToWishList(int stayId, int wishlistId)
    {
        bool result = await _mediator.Send(new SaveStayToWishlistCommand()
        {
            StayId = stayId,
            WishlistId = wishlistId
        });
        return Ok(result);
    }

    /// <summary>
    /// Add stay to trip/journey
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("{stayId}/add-to-trip/{tripId}")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> AddStayToTrip(int stayId, int tripId)
    {
        bool result = await _mediator.Send(new AddStayToTripCommand()
        {
            StayId = stayId,
            TripId = tripId
        });
        return Ok(result);
    }

    #region CRUD
    /// <summary>
    /// Create new stay
    /// </summary>
    /// <response code="200">Stay Id</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("create-stay")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<int>> CreateStay(CreateStayCommand command)
    {
        int stayId = await _mediator.Send(command);
        return Ok(stayId);
    }

    /// <summary>
    /// Update stay
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPut]
    [Route("update-stay")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> UpdateStay([FromBody]UpdateStayCommand command)
    {
        bool result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete stay
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpDelete]
    [Route("{stayId}/delete-stay")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> DeleteStay(int stayId)
    {
        bool result = await _mediator.Send(new DeleteStayCommand()
        {
            StayId = stayId
        });
        return Ok(result);
    }
    #endregion

    /// <summary>
    /// Search stay
    /// </summary>
    /// <response code="200">List of stays</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost]
    [Route("search")]
    [ProducesResponseType(typeof(IEnumerable<StayViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> SearchStay(SearchStayCommand command)
    {
        IEnumerable<StayViewModel> result = await _mediator.Send(command);
        return Ok(result);
    }
}
