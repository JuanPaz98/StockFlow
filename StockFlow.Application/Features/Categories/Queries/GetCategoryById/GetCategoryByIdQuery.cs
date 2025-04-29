using MediatR;

public record GetCategoryByIdQuery(int Id) : IRequest<Result<CategoryIdDto>>;
