using CardsServer.BLL.Entity;
using System.Security.Claims;

namespace CardsServer.BLL.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GetRefreshToken();
    }
}