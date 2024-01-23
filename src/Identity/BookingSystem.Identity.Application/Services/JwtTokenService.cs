using BookingSystem.Identity.Application.Abstractions;
using BookingSystem.Identity.Application.Models;
using Microsoft.AspNetCore.Authentication;

namespace BookingSystem.Identity.Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public AuthenticationToken? GenerateAuthToken(RequestReponseModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
