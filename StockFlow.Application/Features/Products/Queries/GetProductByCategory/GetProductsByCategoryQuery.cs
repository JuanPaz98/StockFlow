
using MediatR;
using StockFlow.Application.Features.Dtos.Products;

public record GetProductsByCategoryQuery(int CategoryId) : IRequest<Result<IEnumerable<ProductResponseDto>>>;