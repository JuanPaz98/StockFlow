
using MediatR;
using StockFlow.Application.Features.Dtos.Suppliers;

public record GetSupplierByIdQuery(int Id) : IRequest<Result<SupplierResponseDto>>;
