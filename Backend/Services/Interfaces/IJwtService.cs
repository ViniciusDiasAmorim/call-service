using CallServiceFlow.Model;
using System.Security.Claims;

namespace CallServiceFlow.Services
{
    public interface IJwtService
    {
        Task<TokenResponse> GenerateTokenAsync(ApplicationUser user);
        Task<TokenResponse> RefreshTokenAsync(string token, string refreshToken);
    }
}
