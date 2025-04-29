
using MediatR;
using StockFlow.Application.Features.Dtos.Payments;

public record GetPaymentsByOrderIdQuery(int Id) : IRequest<Result<IEnumerable<PaymentResponseDto>>>;
