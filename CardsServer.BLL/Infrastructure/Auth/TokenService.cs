﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CardsServer.BLL.Infrastructure.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;
        private readonly IUserRepository _userRepository;

        public TokenService(IOptions<JwtOptions> options, IUserRepository userRepository)
        {
            _options = options.Value;
            _userRepository = userRepository;
        }

        public string GenerateAccessToken(UserEntity user)
        {

            List<Claim> claims;

            try
            {
                claims =
                [
                    new( ClaimTypes.Name, user.UserName ),
                    new( ClaimTypes.Email, user.Email ),
                    new( ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new( ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
                ];
            }
            catch(Exception ex)
            {
                throw ex;
            }

            claims.AddRange(user.Role.Permissions.Select(p => new Claim("Permissions", p.Title)));

            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes));

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CardsSecretKeyJK(*HJ*(#HUFihBGBbrgjnI()Uj4tjrjkgb")),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken securityToken;

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public DateTime GetRefreshTokenExpiryTime()
        {
            return DateTime.UtcNow.AddDays(30);
        }
    }
}
