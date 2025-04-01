using MediatR;
using StockFlow.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(int id): IRequest<GetProductByIdModel>;



