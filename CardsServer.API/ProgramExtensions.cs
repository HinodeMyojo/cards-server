using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Auth.Roles;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Services;
using CardsServer.BLL.Services.Module;
using CardsServer.BLL.Services.User;
using CardsServer.DAL;
using CardsServer.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CardsServer.API
{
    public static class ProgramExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();

            services.AddTransient<IJwtGenerator, JwtGenerator>();

            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();

            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPolicyService, PolicyService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IImageService, ImageService>();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPolicyRepository, PolicyRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IElementRepostory, ElementRepository>();

            services.AddTransient<IRedisCaching, RedisCaching>();

            return services;
        }

        public static IServiceCollection AuthService(this IServiceCollection services, IConfiguration configuration) 
        {
            JwtOptions jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            if (jwtOptions == null)
            {
                throw new ArgumentNullException(nameof(jwtOptions));
            }

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
            //services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();

            return services;
        }
    }
}
