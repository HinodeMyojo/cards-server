using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public class JwtGenerator
    {
        public async Task<JwtSecurityToken> GenerateToken()
        {
            var claims = new List<Claim> { new Claim { ClaimTypes.Name, username} }
        }
    }
}
