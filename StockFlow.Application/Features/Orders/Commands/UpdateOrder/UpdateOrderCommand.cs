
using MediatR;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderWithIdDto model) : IRequest<OrderWithIdDto>;