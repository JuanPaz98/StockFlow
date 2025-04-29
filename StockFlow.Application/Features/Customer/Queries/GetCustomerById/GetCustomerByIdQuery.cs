using MediatR;
using StockFlow.Application.Features.Dtos.Customers;

public record GetCustomerByIdQuery(int Id) : IRequest<Result<CustomerResponseDto>>;

