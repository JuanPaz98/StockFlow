
using MediatR;

public record UpdateCategoryCommand(CategoryIdDto Data) : IRequest<Result<CategoryIdDto>>;

