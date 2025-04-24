using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IscreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IscreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
