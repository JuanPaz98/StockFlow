using MediatR;
using StockFlow.Application.Features.Dtos.Customers;


public record CreateCustomerCommand(CustomerRequestDto Data) : IRequest<Result<int>>;

