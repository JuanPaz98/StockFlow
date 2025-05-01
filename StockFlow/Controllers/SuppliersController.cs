using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Dtos.Suppliers;
using StockFlow.Application.Features.Suppliers.Commands.DeleteSupplier;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/suppliers")]
    [ApiController]
    public class SuppliersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllSuppliersQuery());

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IValidator<GetSupplierByIdQuery> validator)
        {
            var query = new GetSupplierByIdQuery(id);

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

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SupplierRequestDto data,
            [FromServices] IValidator<CreateSupplierCommand> validator)
        {
            var command = new CreateSupplierCommand(data);

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

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] SupplierRequestIdDto data,
            [FromServices] IValidator<UpdateSupplierCommad> validator)
        {
            var command = new UpdateSupplierCommad(data);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(new UpdateSupplierCommad(data));
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IValidator<DeleteSupplierCommand> validator)
        {
            var command = new DeleteSupplierCommand(id);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
