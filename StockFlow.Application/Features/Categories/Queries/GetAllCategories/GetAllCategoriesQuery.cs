
using MediatR;

public record GetAllCategoriesQuery() : IRequest<Result<IEnumerable<CategoryIdDto>>>;