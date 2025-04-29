using MediatR;
using StockFlow.Application.Features.Dtos.Orders;

public record CreateOrderCommand(OrderRequestDto Data) : IRequest<Result<int>>;