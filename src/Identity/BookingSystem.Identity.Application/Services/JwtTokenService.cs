using BookingSystem.Identity.Application.Abstractions;
using BookingSystem.Identity.Application.Models;
using Microsoft.AspNetCore.Authentication;

namespace BookingSystem.Identity.Application.Services
{
    internal class JwtTokenService : IJwtTokenService
    {
        public AuthenticationToken? GenerateAuthToken(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
