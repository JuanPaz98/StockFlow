using MediatR;
using StockFlow.Application.Features.Dtos.Products;

public record CreateProductCommand(ProductRequestDto Data) : IRequest<Result<int>>;

