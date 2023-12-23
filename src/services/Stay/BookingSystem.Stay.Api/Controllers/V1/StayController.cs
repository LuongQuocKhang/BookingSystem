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

namespace BookingSystem.Stay.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class StaysController(ILogger<StaysController> logger, IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<StaysController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StayViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> GetStays()
    {
        IEnumerable<StayViewModel> result = await _mediator.Send(new GetStaysQuery());
        return Ok(result);
    }

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

    [HttpPost]
    [Route("{stayId}/review-stay")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
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

    [HttpPost]
    [Route("{stayId}/share-stay")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> ShareStay(int stayId, List<int> Recipients)
    {
        bool result = await _mediator.Send(new ShareStayCommand()
        {
            Recipients = Recipients,
            StayId = stayId
        });
        return Ok(result);
    }

    [HttpPost]
    [Route("{stayId}/add-to-wishlist/{wishlistId}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> SaveStayToWishList(int stayId, int wishlistId)
    {
        bool result = await _mediator.Send(new SaveStayToWishlistCommand()
        {
            StayId = stayId,
            WishlistId = wishlistId
        });
        return Ok(result);
    }

    [HttpPost]
    [Route("{stayId}/add-to-trip/{tripId}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
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

    [HttpPost]
    [Route("create-stay")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<int>> CreateStay(CreateStayCommand command)
    {
        int stayId = await _mediator.Send(command);
        return Ok(stayId);
    }

    [HttpPut]
    [Route("update-stay")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<int>> UpdateStay([FromBody]UpdateStayCommand command)
    {
        bool result = await _mediator.Send(command);
        return Ok(result);
    }

    // Delete Stay
    [HttpDelete]
    [Route("{stayId}/delete-stay")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> DeleteStay(int stayId)
    {
        bool result = await _mediator.Send(new DeleteStayCommand()
        {
            StayId = stayId
        });
        return Ok(result);
    }
    #endregion

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
