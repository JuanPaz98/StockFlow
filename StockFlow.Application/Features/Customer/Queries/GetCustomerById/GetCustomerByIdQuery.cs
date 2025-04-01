using MediatR;
using StockFlow.Application.Features.Customer.Queries.GetCustomerById;

public record GetCustomerByIdQuery(int id): IRequest<GetCustomerByIdModel>;

