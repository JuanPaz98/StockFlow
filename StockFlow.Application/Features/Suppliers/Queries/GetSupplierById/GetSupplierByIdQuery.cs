
using MediatR;
using StockFlow.Application.Features.Suppliers.Queries.GetSupplierById;

public record GetSupplierByIdQuery(int Id) : IRequest<GetSupplierByIdModel>;
    

