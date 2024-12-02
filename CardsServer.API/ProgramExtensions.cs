using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Auth.Roles;
using CardsServer.BLL.Infrastructure.RabbitMq;
using CardsServer.BLL.Services;
using CardsServer.BLL.Services.gRPC;
using CardsServer.BLL.Services.Learning;
using CardsServer.BLL.Services.Module;
using CardsServer.BLL.Services.User;
using CardsServer.DAL;
using CardsServer.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using StatisticService.API;
using System.Text;

namespace CardsServer.API
{
    public static class ProgramExtensions
    {
        /// <summary>
        /// Extension метод для регистрации сервисов.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();

            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPolicyService, PolicyService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IElementService, ElementService>();
            //services.AddTransient<IStatisticService, BLL.Services.Cards.StatisticService>();
            services.AddTransient<ILearningService, LearningService>();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPolicyRepository, PolicyRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IElementRepostory, ElementRepository>();
            //services.AddTransient<IStatisticRepository, StatisticRepository>();
            services.AddTransient<ILearningRepository, LearningRepository>();

            services.AddTransient<IRedisCaching, RedisCaching>();

            // Интеграция фабрики клиента gRPC
            services.AddGrpcClient<Statistic.StatisticClient>(o =>
            {
                o.Address = new Uri("http://statistic-service:8080");
            });
            //.EnableCallContextPropagation();
            services.AddTransient<BLL.Services.gRPC.StatisticService>();

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
