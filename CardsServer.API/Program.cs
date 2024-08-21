using CardsServer.API;
using CardsServer.API.Middlewares;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterService();
builder.Services.AuthService(configuration);
builder.Services.Configure<JwtOptions>(
    configuration.GetSection(nameof(JwtOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext =
//        scope.ServiceProvider
//            .GetRequiredService<ApplicationContext>();
//    dbContext.Database.Migrate();
//}

app.Run();
