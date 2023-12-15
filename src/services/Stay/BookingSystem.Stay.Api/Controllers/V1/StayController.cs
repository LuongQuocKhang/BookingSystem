#region Using Commands, queries
using BookingSystem.Stay.Application.Handlers.Commands.DeleteStay;
using BookingSystem.Stay.Application.Handlers.Commands.ReviewStay;
using BookingSystem.Stay.Application.Handlers.Commands.SaveStayToWishlist;
using BookingSystem.Stay.Application.Handlers.Commands.SearchStay;
using BookingSystem.Stay.Application.Handlers.Commands.ShareStay;
using BookingSystem.Stay.Application.Handlers.Queries.GetStayDetails;
using BookingSystem.Stay.Application.Features.Commands.CreateStay;
using BookingSystem.Stay.Application.Handlers.Commands.AddStayToTrip;
using BookingSystem.Stay.Application.Handlers.Commands.UpdateStay;
using BookingSystem.Stay.Application.Handlers.Queries.GetStays;
using BookingSystem.Stay.Application.ViewModel;
#endregion

using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Stay.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class StaysController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<StaysController> _logger;
    public StaysController(ILogger<StaysController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StayViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StayViewModel>>> GetStays()
    {
        IEnumerable<StayViewModel> result = await _mediator.Send(new GetStaysQuery());
        return Ok(result);
    }

    [HttpGet("{stayId}")]
    [ProducesResponseType(typeof(StayViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StayViewModel>> GetStayDetails(int stayId)
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
    public async Task<ActionResult<bool>> ReviewStay(int stayId, ReviewStayViewModel model)
    {
        ReviewStayCommand command = new ReviewStayCommand()
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
    public async Task<ActionResult<int>> UpdateStay(UpdateStayCommand command)
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
