
using MediatR;
using StockFlow.Application.Features.Suppliers.Queries.GetAllSuppliers;

public record GetAllSuppliersQuery(): IRequest<IEnumerable<GetAllSuppliersModel>>;

