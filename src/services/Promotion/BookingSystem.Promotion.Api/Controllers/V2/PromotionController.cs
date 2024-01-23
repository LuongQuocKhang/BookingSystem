using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Promotion.Api.Controllers.V2
{

    /// <summary>
    /// Promotion Management
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/promotions")]
    [Authorize]
    public class PromotionController : ControllerBase
    {
    }
}
