
using MediatR;
using StockFlow.Application.Features.Payments.Queries.GetPaymentsByOrderId;

public record GetPaymentsByOrderIdQuery(int Id): IRequest<IEnumerable<GetPaymentsByOrderIdModel>>;
