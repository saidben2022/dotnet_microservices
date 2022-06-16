using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]//not necessary to add variable inside HttpGet but adding them would show them in swager api giving the api a more readable form
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);
            if (coupon == null)
            {
                return NotFound();
            }
            return coupon;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<Coupon>> CreateDiscount(Coupon coupon)
        {
            if (await _discountRepository.CreateDiscount(coupon))
            {
                return CreatedAtAction(nameof(GetDiscount), new { productName = coupon.ProductName }, coupon);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<Coupon>> UpdateDiscount(Coupon coupon)
        {
            if (await _discountRepository.UpdateDiscount(coupon))
            {
                return Ok(coupon);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{productName}",Name = "DeleteDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            if (await _discountRepository.DeleteDiscount(productName))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


    }
}
