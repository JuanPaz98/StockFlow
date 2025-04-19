using MediatR;
using StockFlow.Application.Features.Dtos.Orders;

public record CreateOrderCommand(OrderRequestDto model) : IRequest<int>;