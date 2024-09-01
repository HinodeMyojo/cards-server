using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Services.User;
using CardsServer.DAL;
using CardsServer.DAL.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CardsServer.API
{
    public static class ProgramExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();

            services.AddTransient<IJwtGenerator, JwtGenerator>();


            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPermissionService, PermissionService>();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();

            

            return services;
        }

        public static IServiceCollection AuthService(this IServiceCollection services, IConfiguration configuration) 
        {
            JwtOptions jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    // Чтобы claims токенов не переименовались (нужно для поиска id по claims JwtRegisteredClaimNames.Sub
                    opt.MapInboundClaims = false;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)),
                        ValidateLifetime = true,
                    };
                });

            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }
    }
}
