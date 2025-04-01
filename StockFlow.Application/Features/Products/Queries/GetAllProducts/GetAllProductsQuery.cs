
using MediatR;
using StockFlow.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(): IRequest<IEnumerable<GetAllProductsModel>>;

