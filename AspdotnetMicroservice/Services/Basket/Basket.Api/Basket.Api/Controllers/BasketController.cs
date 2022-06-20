using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //you need to add the [ApiController] to the controller where you extend ControllerBase. The [FromBody] is only needed if you're doing an MVC controller.

   // This causes the body to get automatically processed in the way you're expecting
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

    private readonly DiscountGrpcService _discountGrpcService;//Make sure to Add DiscountGrpcService in Program.cs
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            
            
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
        public async Task<ActionResult<ShopingCart>> UpdateBasket([FromBody]ShopingCart shoppingCart)//{Frombody] is not necessary look above for explination
        {
            //To do: Communicate with Discount Grpc and calculate the discount for the basket
            //right click the  current project add ->Connected Service->add service reference->grpc->under file browse to the discountprotofile ->and under type of class generated  choose Client
            foreach (var item in shoppingCart.Items)
            {
                var coupon = _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Result.Amount;
            }
            return Ok(await _basketRepository.UpdateBasket(shoppingCart));
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
