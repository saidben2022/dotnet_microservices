using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShopingCart>> GetBasket(string Username)
        {
            var basket = await _basketRepository.GetShopingCart(Username);
            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ShopingCart>> UpdateBasket(ShopingCart shoppingCart)
        {
            await _basketRepository.UpdateBasket(shoppingCart);
            return CreatedAtAction(nameof(GetBasket), new { Username = shoppingCart.Username }, shoppingCart);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShopingCart>> DeleteBasket(string Username)
        {
            await _basketRepository.DeleteBasket(Username);
            return Ok();
        }
    }
}
