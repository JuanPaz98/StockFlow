using MediatR;
using StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId;


public record GetOrdersByCustomerIdQuery(int id): IRequest<List<GetOrdersByCustomerIdModel>>;

