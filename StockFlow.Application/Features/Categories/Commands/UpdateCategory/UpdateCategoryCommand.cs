
using MediatR;

public record UpdateCategoryCommand(CategoryDto Model) : IRequest<CategoryDto>;

