using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Categories.Commands.DeleteCategory;
using StockFlow.Application.Features.Dtos.Categories;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCategoriesQuery());

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IValidator<GetCategoryByIdQuery> validator)
        {
            var query = new GetCategoryByIdQuery(id);

            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(new GetCategoryByIdQuery(id));
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CategoryDto request,
            [FromServices] IValidator<CreateCategoryCommand> validator)
        {
            var command = new CreateCategoryCommand(request);

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
            [FromBody] CategoryIdDto request,
            [FromServices] IValidator<UpdateCategoryCommand> validator)
        {
            var command = new UpdateCategoryCommand(request);

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

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IValidator<DeleteCategoryCommand> validator)
        {
            var command = new DeleteCategoryCommand(id);

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

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}