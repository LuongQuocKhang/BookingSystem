using System.Net;

namespace BookingSystem.Identity.Application.Models;

public class RequestReponseModel
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public object Content { get; set; }
}
