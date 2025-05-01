using MediatR;

namespace StockFlow.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest<Result<bool>>;
    
}
