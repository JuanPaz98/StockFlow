using MediatR;
using StockFlow.Application.Features.Dtos.Suppliers;

public record CreateSupplierCommand(SupplierRequestDto Data) : IRequest<Result<int>>;

