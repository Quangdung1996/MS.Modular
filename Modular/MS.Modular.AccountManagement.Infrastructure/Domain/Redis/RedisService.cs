using Microsoft.Extensions.Caching.Distributed;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.Redis;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.Redis
{
    internal class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;

        internal RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
    

        public async Task<T> GetAsync<T>(string key)
        {
            var dataStr = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(dataStr))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(dataStr);
        }

        public Task SetAsync<T>(string key, T data)
        {
            var dataJson = JsonConvert.SerializeObject(data, GlobalSetting.JsonSetting);
            return _distributedCache.SetStringAsync(key, dataJson);
        }

        public Task RemoveAsync(string key)
        {
            return _distributedCache.RemoveAsync(key);
        }
    }
}