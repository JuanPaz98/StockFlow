using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Customer.Commands.CreateCustomer;
using StockFlow.Application.Features.Customer.Commands.UpdateCustomer;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-customers")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(customer);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCustomerModel model)
        {
            var customer = await _mediator.Send(new CreateCustomerCommand(model));
            if (customer == null)
            {
                return BadRequest();
            }
            return StatusCode(201);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCustomerModel model)
        {
            var customer = await _mediator.Send(new UpdateCustomerCommand(model));
            if (customer == null)
            {
                return BadRequest();
            }
            return StatusCode(StatusCodes.Status200OK, model);
        }
    }
}
