
using MediatR;
using StockFlow.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(UpdateProductModel Model) : IRequest<UpdateProductModel>;   

