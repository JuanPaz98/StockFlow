using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Customer.Commands.CreateCustomer;
using StockFlow.Application.Features.Customer.Queries.GetAllCustomers;

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
        public async Task<ActionResult<IEnumerable<GetAllCustomersModel>>> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        [HttpPost("create-customer")]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateCustomerModel model)
        {
            var customerId = await _mediator.Send(new CreateCustomerCommand(model));
            if (customerId == null)
            {
                return BadRequest();
            }
            return StatusCode(201, customerId);
        }


    }
}
