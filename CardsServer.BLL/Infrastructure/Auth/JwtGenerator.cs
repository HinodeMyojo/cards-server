using CardsServer.BLL.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public class JwtGenerator
    {
        private readonly JwtOptions _options;

        public JwtGenerator(JwtOptions options)
        {
            _options = options;
        }

        public async Task<string> GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new( ClaimTypes.Name, user.UserName ),
                new( ClaimTypes.Email, user.Email )
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;

        }  
    }
}
