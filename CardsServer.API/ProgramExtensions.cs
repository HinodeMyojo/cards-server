using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CardsServer.API
{
    public static class ProgramExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        public static IServiceCollection AuthService(this IServiceCollection services, IConfiguration configuration) 
        {
            JwtOptions? jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "",
                        ValidAudience = "",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };
                });

            return services;
        }
    }
}
