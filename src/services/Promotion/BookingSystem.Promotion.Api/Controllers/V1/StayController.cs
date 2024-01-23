using BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionsByStay;
using BookingSystem.Promotion.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingSystem.Promotion.Api.Controllers.V1;

/// <summary>
/// Stay Promotion Management
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/stay")]
[Authorize]
public class StayController(IMediator mediator, ILogger<PromotionController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<PromotionController> _logger = logger;

    /// <summary>
    /// Get list of promotions
    /// </summary>
    /// <response code="200">Return List of promotions </response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpGet("{stayId}/promotions")]
    [ProducesResponseType(typeof(IReadOnlyCollection<PromotionViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyCollection<PromotionViewModel>>> GetPromotions(int stayId)
    {
        IReadOnlyCollection<PromotionViewModel> result = await _mediator.Send(new GetPromotionsByStayQuery()
        {
            StayId = stayId
        });
        return Ok(result);
    }

}
