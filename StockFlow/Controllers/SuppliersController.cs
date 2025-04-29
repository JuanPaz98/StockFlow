//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using StockFlow.Application.Features.Suppliers.Commands.CreateSupplier;
//using StockFlow.Application.Features.Suppliers.Commands.UpdateSupplier;

//namespace StockFlow.Api.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class SuppliersController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public SuppliersController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        [HttpGet("get-all-suppliers")]
//        public async Task<IActionResult> GetAll()
//        {
//            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());

//            return Ok(suppliers);
//        }

//        [HttpGet("get-by-id/{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));

//            return Ok(supplier);
//        }

//        [HttpPost("create")]
//        public async Task<IActionResult> Create(
//            [FromBody] CreateSupplierModel model)
//        {
//            var result = await _mediator.Send(new CreateSupplierCommand(model));
//            return Ok(result);
//        }

//        [HttpPut("update")]
//        public async Task<IActionResult> Update(
//            [FromBody] UpdateSupplierModel model)
//        {
//            var result = await _mediator.Send(new UpdateSupplierCommad(model));
//            return Ok(result);
//        }
//    }
//}
