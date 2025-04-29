
using MediatR;
using StockFlow.Application.Features.Dtos.Products;

public record UpdateProductCommand(ProductRequestIdDto Data) : IRequest<Result<ProductRequestIdDto>>;

