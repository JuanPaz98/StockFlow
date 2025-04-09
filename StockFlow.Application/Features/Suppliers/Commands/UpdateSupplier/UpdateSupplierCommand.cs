
using MediatR;
using StockFlow.Application.Features.Suppliers.Commands.UpdateSupplier;

public record UpdateSupplierCommad(UpdateSupplierModel Model) : IRequest<UpdateSupplierModel>;