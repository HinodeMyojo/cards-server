using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Abstractions
{
    public interface IJwtGenerator
    {
        string GenerateToken(UserEntity user);
    }
}