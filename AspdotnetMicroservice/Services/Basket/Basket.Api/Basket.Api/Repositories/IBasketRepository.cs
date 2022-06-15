using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        Task<ShopingCart> GetShopingCart(string Username);
        Task<ShopingCart> UpdateBasket(ShopingCart shoppingCart);

        Task DeleteBasket(string Username);
    }
}
