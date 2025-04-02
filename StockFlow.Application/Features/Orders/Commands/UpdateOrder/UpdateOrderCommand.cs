
using MediatR;
using StockFlow.Application.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(UpdateOrderModel model) : IRequest<UpdateOrderModel>;