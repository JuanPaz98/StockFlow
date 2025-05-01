using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Dtos.Products;
using StockFlow.Application.Features.Products.Commands.DeleteProduct;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllProductsQuery());
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IValidator<GetProductByIdQuery> validator)
        {
            var query = new GetProductByIdQuery(id);

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

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(
            int categoryId,
            [FromServices] IValidator<GetProductsByCategoryQuery> validator)
        {
            var query = new GetProductsByCategoryQuery(categoryId);

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
            [FromBody] ProductRequestDto data,
            [FromServices] IValidator<CreateProductCommand> validator)
        {
            var command = new CreateProductCommand(data);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Error);
            }

            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] ProductRequestIdDto data,
            [FromServices] IValidator<UpdateProductCommand> validator)
        {
            var command = new UpdateProductCommand(data);

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IValidator<DeleteProductCommand> validator)
        {
            var command = new DeleteProductCommand(id);

            var validationResult = await validator.ValidateAsync(command);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result.Error);
            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
