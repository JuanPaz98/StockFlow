using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Dtos.Payments;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/payments")]
    [ApiController]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var result = await mediator.Send(new GetPaymentsByOrderIdQuery(orderId));
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PaymentRequestDto data,
            [FromServices] IValidator<CreatePaymentCommand> validator)
        {
            var command = new CreatePaymentCommand(data);

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

            return StatusCode(201);
        }
    }
}
