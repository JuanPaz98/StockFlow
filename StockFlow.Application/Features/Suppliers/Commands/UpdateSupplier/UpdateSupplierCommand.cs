
using MediatR;
using StockFlow.Application.Features.Dtos.Suppliers;

public record UpdateSupplierCommad(SupplierRequestIdDto Data) : IRequest<Result<SupplierRequestIdDto>>;