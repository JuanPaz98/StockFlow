using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Orders.Commands.CreateOrder;
using StockFlow.Application.Features.Orders.Commands.UpdateOrder;
using StockFlow.Application.Features.Orders.Dtos;

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
            [FromBody] OrderRequestDto model)
        {
            var result = await _mediator.Send(new CreateOrderCommand(model));

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(
           int id)
        {
            var orders = await _mediator.Send(new GetOrderByIdQuery(id));

            return Ok(orders);
        }

        [HttpGet("get-orders-by-customer-id/{id}")]
        public async Task<IActionResult> GetByCustomerId(
            int id)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery(id));

            return Ok(orders); 
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateOrderModel model)
        {
            var result = await _mediator.Send(new UpdateOrderCommand(model));

            return Ok(result);
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));

            return Ok(result);
        }

        [HttpDelete("delete-order-details/{id}")]
        public async Task<IActionResult> DeleteOrderDetails(int id)
        {
            var result = await _mediator.Send(new DeleteOrderDetailsCommand(id));

            return Ok(result);
        }
    }
}
