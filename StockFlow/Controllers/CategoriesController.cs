using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Categories.Commands.CreateCategory;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryModel model)
        {
            var result = await _mediator.Send(new CreateCategoryCommand(model));
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] CategoryDto model)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(model));
            return Ok(result);
        }
    }
}
