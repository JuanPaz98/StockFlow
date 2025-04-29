
using MediatR;
using StockFlow.Application.Features.Dtos.Orders;

public record UpdateOrderCommand(OrderWithIdDto Data) : IRequest<Result<OrderWithIdDto>>;