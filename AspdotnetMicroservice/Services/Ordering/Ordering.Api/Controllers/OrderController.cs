using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }
       //DO NOT FORGET THE AWAIT OR IT WOULD CAUSE PROBLEMS FOR DAYS
        [HttpGet("{username}",Name = "GetOrderByUsername")]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>),StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrderByUsername(string username)
        {
            var query=new GetOrderListQuery(username);
            var orders = _mediator.Send(query);
            return Ok(await orders);//AWAIT IS VERY IMPORTANT HERE 

        }
        [HttpPost(Name ="checkoutOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CheckoutOrderCommand([FromBody] CheckoutOrderCommand ordercommand)
        {
            var result = _mediator.Send(ordercommand);
            return Ok(await result);

        }
        [HttpPut(Name = "updateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<int>> UpdateOrder([FromBody] UpdateOrderCommand UpdateOrdercommand)
        {
            var result = _mediator.Send(UpdateOrdercommand);
            return NoContent();
        }
    }
}
