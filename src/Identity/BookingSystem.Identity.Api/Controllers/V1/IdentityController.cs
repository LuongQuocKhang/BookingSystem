using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Identity.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IdentityController(ILogger<IdentityController> logger) : ControllerBase
    {

        private readonly ILogger<IdentityController> _logger = logger;

        [HttpPost("/login")]
        public IActionResult Login()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpPost("/register")]
        public IActionResult Register()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpPost("/token")]
        public IActionResult Token()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
