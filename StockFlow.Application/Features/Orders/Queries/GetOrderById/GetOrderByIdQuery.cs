
using MediatR;
using StockFlow.Application.Features.Dtos.Orders;

public record GetOrderByIdQuery(int Id) : IRequest<Result<OrderWithIdDto>>;

