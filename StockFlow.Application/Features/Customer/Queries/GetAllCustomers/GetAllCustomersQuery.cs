using MediatR;
using StockFlow.Application.Features.Dtos.Customers;

public record GetAllCustomersQuery() : IRequest<Result<IEnumerable<CustomerResponseDto>>>;

