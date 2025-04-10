﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Features.Products.Commands.CreateProduct;
using StockFlow.Application.Features.Products.Commands.UpdateProduct;

namespace StockFlow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());

            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            return StatusCode(StatusCodes.Status200OK, product);
        }
        
        [HttpGet("get-by-category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _mediator.Send(new GetProductsByCategoryQuery(category));

            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductModel model)
        {
            var result = await _mediator.Send(new CreateProductCommand(model));

            if (result == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateProductModel model)
        {
            var result = await _mediator.Send(new UpdateProductCommand(model));
            if (result is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, model);
        }
    }
}
