
using MediatR;
using StockFlow.Application.Features.Products.Queries.GetProductByCategory;

public record GetProductsByCategoryQuery(string category): IRequest<IEnumerable<GetProductByCategoryModel>>;

