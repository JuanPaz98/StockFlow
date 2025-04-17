
using MediatR;
using StockFlow.Application.Features.Products.Queries.GetProductByCategory;

public record GetProductsByCategoryQuery(int categoryId): IRequest<IEnumerable<GetProductByCategoryModel>>;

