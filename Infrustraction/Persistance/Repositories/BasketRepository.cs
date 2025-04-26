using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var createdOrUpdated =  await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if(!createdOrUpdated)
                return null;
            else
                return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id) => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var basket = await _database.StringGetAsync(Key);
            if(basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
