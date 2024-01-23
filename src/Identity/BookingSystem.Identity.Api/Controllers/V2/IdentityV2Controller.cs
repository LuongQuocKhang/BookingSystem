using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Identity.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class IdentityV2Controller(ILogger<IdentityV2Controller> logger) : ControllerBase
    {

        private readonly ILogger<IdentityV2Controller> _logger = logger;

        [HttpPost("/login")]
        public IActionResult LoginV2()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
