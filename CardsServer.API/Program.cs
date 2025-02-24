using CardsServer.API;
using CardsServer.API.Extension;
using CardsServer.API.Middlewares;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.DAL;
using CardsServer.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cards", Version = "0.0.1"});
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer YOUR TOKEN HERE'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }, 
            },
            Array.Empty<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Load Env
if (Environment.GetEnvironmentVariable("CONNECTION_STRING").IsNullOrEmpty())
{
    DotNetEnv.Env.Load();
}

builder.Services.RegisterService();
builder.Services.AuthService(configuration);
builder.Services.Configure<JwtOptions>(
    configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddDebug();
    logging.AddProvider(
        new DbLoggerProvider(
            builder.Services.BuildServiceProvider().
            GetRequiredService<ILogRepository>()));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379,password=admin";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext =
        scope.ServiceProvider
            .GetRequiredService<ApplicationContext>();
        dbContext.Database.Migrate();
    }
    catch(Exception ex)
    {
        throw new Exception($"Не удалось обновить базу данных. {ex}");
    }
    
}

app.Run();  
