
using MediatR;
using StockFlow.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<GetOrderByIdModel>;

