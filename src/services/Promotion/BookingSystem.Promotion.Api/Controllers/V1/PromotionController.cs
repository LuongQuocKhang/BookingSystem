#region Commands, queries
using BookingSystem.Promotion.Application.Features.Promotion.Commands.CreatePromotion;
using BookingSystem.Promotion.Application.Features.Promotion.Commands.DeletePromotion;
using BookingSystem.Promotion.Application.Features.Promotion.Commands.UpdatePromotion;
using BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionDetail;
using BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotions;
using BookingSystem.Promotion.Application.Features.Promotion.Queries.GetPromotionsByStay;
using BookingSystem.Promotion.Application.ViewModel;
#endregion

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BookingSystem.Promotion.Api.Controllers.V1;

/// <summary>
/// Promotion Management
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/promotions")]
[Authorize]
public class PromotionController(IMediator mediator, ILogger<PromotionController> logger) : ControllerBase
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
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<PromotionViewModel>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyCollection<PromotionViewModel>>> GetPromotions()
    {
        IReadOnlyCollection<PromotionViewModel> result = await _mediator.Send(new GetPromotionsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get promotion detail
    /// </summary>
    /// <response code="200">Return promotion detail</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpGet("{promotionId}")]
    [ProducesResponseType(typeof(PromotionViewModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyCollection<PromotionViewModel>>> GetPromotionDetail([Required] int promotionId)
    {
        PromotionViewModel result = await _mediator.Send(new GetPromotionDetailQuery()
        {
            PromotionId = promotionId
        });
        return Ok(result);
    }

    /// <summary>
    /// Create promotion
    /// </summary>
    /// <response code="200">Promotion Id </response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPost("create-promotion")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreatePromotion(CreatePromotionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Update promotion
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpPut("update-promotion")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> UpdatePromotion(UpdatePromotionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete promotion
    /// </summary>
    /// <response code="200">True/False</response>
    /// <response code="400">This response will be returned if the request is invalid or missing required information.</response>
    /// <response code="401">This response will be returned if the request is not authorized to access this resource.</response>
    /// <response code="500">This response will be returned if there is an error in the system that prevents the purchase from being completed.</response>
    [HttpDelete("{promotionId}/delete-promotion")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> DeletePromotion(int promotionId)
    {
        var result = await _mediator.Send(new DeletePromotionCommand()
        {
            PromotionId = promotionId
        });
        return Ok(result);
    }
}
