using MediatR;
using StockFlow.Application.Features.Orders.Dtos;

public record CreateOrderCommand(OrderRequestDto model) : IRequest<OrderResponseDto>;