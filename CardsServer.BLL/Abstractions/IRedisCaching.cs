
namespace CardsServer.BLL.Abstractions
{
    public interface IRedisCaching
    {
        Task<string> GetValueAsync(string key);
        Task SetValueAsync(string key, string value);
    }
}