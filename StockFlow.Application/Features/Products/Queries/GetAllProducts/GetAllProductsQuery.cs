
using MediatR;
using StockFlow.Application.Features.Dtos.Products;

public record GetAllProductsQuery() : IRequest<Result<IEnumerable<ProductResponseDto>>>;

