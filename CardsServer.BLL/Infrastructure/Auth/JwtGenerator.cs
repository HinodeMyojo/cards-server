using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtOptions _options;

        public JwtGenerator(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new( ClaimTypes.Name, user.UserName ),
                new( ClaimTypes.Email, user.Email ),
                new( ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
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

        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
        //        ValidateLifetime = false 
        //    };

        //    JwtSecurityTokenHandler tokenHandler = new();
        //    SecurityToken securityToken;

        //    ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        //    JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //        throw new SecurityTokenException("Invalid token");

        //    return principal;
        //}
    }
}
