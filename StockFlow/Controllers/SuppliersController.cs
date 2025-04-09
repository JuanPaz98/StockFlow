using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-suppliers")]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());

            return Ok(suppliers);
        } 
        
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));

            return Ok(supplier);
        }
    }
}
