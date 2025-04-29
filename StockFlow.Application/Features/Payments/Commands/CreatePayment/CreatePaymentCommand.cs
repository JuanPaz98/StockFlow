using MediatR;
using StockFlow.Application.Features.Dtos.Payments;

public record CreatePaymentCommand(PaymentRequestDto Data) : IRequest<Result<int>>;
