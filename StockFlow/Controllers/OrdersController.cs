using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Dtos.Orders;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] OrderRequestDto data,
            [FromServices] IValidator<CreateOrderCommand> validator)
        {
            var command = new CreateOrderCommand(data);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
           int id,
           [FromServices] IValidator<GetOrderByIdQuery> validator)
        {
            var query = new GetOrderByIdQuery(id);

            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(new GetOrderByIdQuery(id));
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetByCustomerId(
            int id,
            [FromServices] IValidator<GetOrdersByCustomerIdQuery> validator)
        {
            var query = new GetOrdersByCustomerIdQuery(id);

            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(query);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] OrderWithIdDto data,
            [FromServices] IValidator<UpdateOrderCommand> validator)
        {
            var command = new UpdateOrderCommand(data);

            var resultValidation = await validator.ValidateAsync(command);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IValidator<DeleteOrderCommand> validator)
        {
            var command = new DeleteOrderCommand(id);

            var resultValidation = await validator.ValidateAsync(command);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        //[HttpDelete("{id}/details/{detailId}")]
        //public async Task<IActionResult> DeleteOrderDetails(
        //    int id, 
        //    int detailId)
        //{
        //    var result = await mediator.Send(new DeleteOrderDetailsCommand(id));

        //    return Ok(result);
        //}
    }
}
