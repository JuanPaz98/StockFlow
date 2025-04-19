using MediatR;
using StockFlow.Application.Features.Payments.Commands.CreatePayment;

public record CreatePaymentCommand(CreatePaymentModel Model) : IRequest<int>;
