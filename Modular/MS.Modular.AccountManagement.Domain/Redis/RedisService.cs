using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Domain.Redis
{
    public interface IRedisService
    {
        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T data);

        Task RemoveAsync(string key);
    }
}
