using BookingSystem.Identity.Application.Models;
using Microsoft.AspNetCore.Authentication;

namespace BookingSystem.Identity.Application.Abstractions;

public interface IJwtTokenService
{
    AuthenticationToken? GenerateAuthToken(RequestReponseModel loginModel);
}
