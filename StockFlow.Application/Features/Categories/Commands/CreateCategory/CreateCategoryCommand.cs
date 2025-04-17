
using MediatR;
using StockFlow.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(CreateCategoryModel Model) : IRequest<int>;

