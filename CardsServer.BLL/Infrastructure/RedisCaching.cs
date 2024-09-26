using CardsServer.BLL.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace CardsServer.BLL.Infrastructure
{
    public class RedisCaching : IRedisCaching
    {
        private readonly IDistributedCache _cache;

        public RedisCaching(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetValueAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
            Console.WriteLine(value);
        }

        public async Task<string> GetValueAsync(string key)
        {
            string? response = await _cache.GetStringAsync(key);
            Console.WriteLine("Использован кэш");
            return response;
            
        }

        public async Task RemoveValueAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

    }
}
