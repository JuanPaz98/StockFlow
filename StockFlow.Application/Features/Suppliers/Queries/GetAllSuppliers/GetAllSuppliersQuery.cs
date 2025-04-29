
using MediatR;
using StockFlow.Application.Features.Dtos.Suppliers;

public record GetAllSuppliersQuery() : IRequest<Result<IEnumerable<SupplierResponseDto>>>;

