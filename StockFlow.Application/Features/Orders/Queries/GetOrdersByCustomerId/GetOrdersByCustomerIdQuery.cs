using MediatR;
using StockFlow.Application.Features.Dtos.Orders;


public record GetOrdersByCustomerIdQuery(int Id) : IRequest<Result<IEnumerable<OrderWithIdDto>>>;

