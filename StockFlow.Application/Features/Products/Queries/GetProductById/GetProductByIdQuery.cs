using MediatR;
using StockFlow.Application.Features.Dtos.Products;

public record GetProductByIdQuery(int Id) : IRequest<Result<ProductResponseDto>>;
