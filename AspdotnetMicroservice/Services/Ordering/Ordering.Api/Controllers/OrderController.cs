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
      
       /// ///////////////////////////DO NOT FORGET THE AWAIT OR IT WOULD CAUSE PROBLEMS FOR DAYS///////////////////////////////////////////////////////////////
    


        [HttpGet("{username}",Name = "GetOrderByUsername")]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>),StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrderByUsername(string username)
        {
            var query=new GetOrderListQuery(username);
            var orders = await _mediator.Send(query);//AWAIT IS VERY IMPORTANT HERE 
            return Ok( orders);

        }
        [HttpPost(Name ="checkoutOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CheckoutOrderCommand([FromBody] CheckoutOrderCommand ordercommand)
        {
            var result = await _mediator.Send(ordercommand);//AWAIT IS VERY IMPORTANT HERE 
            return Ok( result);

        }
        [HttpPut(Name = "updateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<int>> UpdateOrder([FromBody] UpdateOrderCommand UpdateOrdercommand)
        {
            var result = await _mediator.Send(UpdateOrdercommand);//AWAIT IS VERY IMPORTANT HERE 
            return NoContent();
        }
    }
}
