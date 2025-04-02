
using MediatR;
using StockFlow.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(UpdateProductModel model) : IRequest<UpdateProductModel>;   

