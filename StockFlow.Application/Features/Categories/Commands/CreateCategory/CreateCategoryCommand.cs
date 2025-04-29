
using MediatR;
using StockFlow.Application.Features.Dtos.Categories;

public record CreateCategoryCommand(CategoryDto Data) : IRequest<Result<int>>;

