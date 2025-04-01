using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Orders.Commands.CreateOrder;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderModel model)
        {
            var result = await _mediator.Send(new CreateOrderCommand(model));

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok(result);
        }

        [HttpGet("get-orders-by-customer-id/{id}")]
        public async Task<IActionResult> GetByCustomerId(
            int id)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery(id));

            return Ok(orders); 
        }
    }
}
