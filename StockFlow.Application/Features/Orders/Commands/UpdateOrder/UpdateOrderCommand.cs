
using MediatR;
using StockFlow.Application.Features.Orders.Commands.UpdateOrder;
using StockFlow.Application.Features.Orders.Dtos;

public record UpdateOrderCommand(OrderWithIdDto model) : IRequest<OrderWithIdDto>;