using MediatR;

namespace StockFlow.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int Id) : IRequest<Result<bool>>;
}
