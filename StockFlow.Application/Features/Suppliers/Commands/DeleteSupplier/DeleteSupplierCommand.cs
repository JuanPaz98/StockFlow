using MediatR;

namespace StockFlow.Application.Features.Suppliers.Commands.DeleteSupplier
{
    public record DeleteSupplierCommand(int Id) : IRequest<Result<bool>>; 
}
