using ApplicationCore.Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Infrastructure.CachingServices
{
    public class RedisService : ICacheHandler
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<byte[]> GetCacheValueAsync(string key)
        {
            byte[] result = await _distributedCache.GetAsync(key);

            return result;
        }

        public async void SetCacheValueAsync(string key, byte[] data)
        {
            await _distributedCache.SetAsync(key, data);
        }
    }
}