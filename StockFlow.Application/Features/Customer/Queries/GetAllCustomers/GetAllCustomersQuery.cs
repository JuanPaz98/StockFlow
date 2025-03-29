using MediatR;
using StockFlow.Application.Features.Customer.Queries.GetAllCustomers;

public record GetAllCustomersQuery(): IRequest<List<GetAllCustomersModel>>;

