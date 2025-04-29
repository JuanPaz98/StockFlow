using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Customer.Commands.UpdateCustomer;
using StockFlow.Application.Features.Dtos.Customers;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("customers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IValidator<GetCustomerByIdQuery> validator)
        {
            var query = new GetCustomerByIdQuery(id);
            var validationResult = await validator.ValidateAsync(query);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("customers")]
        public async Task<IActionResult> Create(
            [FromBody] CustomerRequestDto data,
            [FromServices] IValidator<CreateCustomerCommand> validator)
        {
            var command = new CreateCustomerCommand(data);
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        [HttpPut("customers")]
        public async Task<IActionResult> Update(
            [FromBody] CustomerRequestIdDto data,
            [FromServices] IValidator<UpdateCustomerCommand> validator)
        {
            var command = new UpdateCustomerCommand(data);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpDelete("customers/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IValidator<DeleteCustomerCommand> validator)
        {
            var command = new DeleteCustomerCommand(id);
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
