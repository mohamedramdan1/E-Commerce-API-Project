using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    internal class CacheService(ICachRepository cachRepository) : ICacheServices
    {
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await cachRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object CacheValue, TimeSpan TimeToLuve)
        {
            var value = JsonSerializer.Serialize(CacheValue);
            await cachRepository.SetAsync(cacheKey, value, TimeToLuve);
        }
    }
}
