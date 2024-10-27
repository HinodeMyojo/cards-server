using CardsServer.BLL.Entity;
using System.Security.Claims;

namespace CardsServer.BLL.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserEntity user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GetRefreshToken();
        DateTime GetRefreshTokenExpiryTime();
    }
}