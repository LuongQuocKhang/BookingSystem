using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Cars.Api.Controllers.V1
{
    /// <summary>
    /// Booking Management
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cars")]
    [Authorize]
    public class CarsController : ControllerBase
    {
    }
}
