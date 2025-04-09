using MediatR;
using StockFlow.Application.Features.Suppliers.Commands.CreateSupplier;

public record CreateSupplierCommand(CreateSupplierModel Model) : IRequest<int>;

