using Asp.Versioning;
using BookingSystem.Identity.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingSystem.Identity.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController(ILogger<AccountController> logger) : ControllerBase
    {

        private readonly ILogger<AccountController> _logger = logger;

        [HttpPost]
        public async Task<ActionResult<RequestReponseModel>> SignInWithEmailAndPasswordAsync([FromBody] SignInModel signInModel)
        {

            var response = new RequestReponseModel();
            try
            {

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(response);
        }
    }
}
