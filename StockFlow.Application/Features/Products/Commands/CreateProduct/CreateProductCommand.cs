using MediatR;
using StockFlow.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductModel Model) : IRequest<int>;

