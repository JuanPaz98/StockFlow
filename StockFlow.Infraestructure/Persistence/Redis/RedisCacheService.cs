using Newtonsoft.Json;
using StackExchange.Redis;
using StockFlow.Application.Interfaces;

namespace StockFlow.Infraestructure.Persistence.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _cache;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(120);

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _cache = redis.GetDatabase();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            await _cache.StringSetAsync(key, serializedValue, expiration ?? _defaultExpiration);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedValue = await _cache.StringGetAsync(key);
            return cachedValue.HasValue ? JsonConvert.DeserializeObject<T>(cachedValue) : default;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
