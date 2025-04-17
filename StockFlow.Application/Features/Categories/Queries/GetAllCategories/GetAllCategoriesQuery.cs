
using MediatR;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;