//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using StockFlow.Application.Features.Payments.Commands.CreatePayment;

//namespace StockFlow.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentsController : ControllerBase
//    {

//        private readonly IMediator _mediador;

//        public PaymentsController(IMediator mediator)
//        {
//            _mediador = mediator;            
//        }

//        [HttpGet("get-payments-by-order-id/{id}")]
//        public async Task<IActionResult> GetByOrderId(int id)
//        {
//            var payments = await _mediador.Send(new GetPaymentsByOrderIdQuery(id));
//            return Ok(payments);
//        }

//        [HttpPost("create")]
//        public async Task<IActionResult> Create(
//            [FromBody] CreatePaymentModel model)
//        {
//            var result = await _mediador.Send(new CreatePaymentCommand(model));
//            if (result == 0)
//            {
//                return BadRequest();
//            }
//            return StatusCode(201);
//        }
//    }
//}
