using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public readonly IDistributedCache _distributedCache;
        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

        }
        public async Task DeleteBasket(string Username)
        {
           await _distributedCache.RemoveAsync(Username);
          
        }

        public async Task<ShopingCart> GetShopingCart(string Username)
        {
            var basket = await _distributedCache.GetStringAsync(Username);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
                return JsonConvert.DeserializeObject<ShopingCart>(basket);
        }

        public async Task<ShopingCart> UpdateBasket(ShopingCart shoppingCart)
        {
            await _distributedCache.SetStringAsync(shoppingCart.Username, JsonConvert.SerializeObject(shoppingCart));
            return await GetShopingCart(shoppingCart.Username);
        }
    }
}
