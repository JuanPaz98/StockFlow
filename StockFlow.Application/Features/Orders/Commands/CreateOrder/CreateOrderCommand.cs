using MediatR;
using StockFlow.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(CreateOrderModel model) : IRequest<CreateOrderModel>;