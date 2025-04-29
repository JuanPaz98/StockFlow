using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Dtos.Categories;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCategoriesQuery());

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("categories/{id}")]
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
            return Ok(result.Value);
        }

        [HttpPost("categories")]
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

            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        [HttpPut("categories")]
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
            return Ok(result.Value);
        }
    }
}
